using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.BusinessLogic.Common.Interfaces;
using FitnessManager.BusinessLogic.User.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.DataAccess.Repositories.Interfaces;
using FitnessManager.Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.BusinessLogic.User
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IBaseRepository<ContactEntity> _contactRepository;
        private readonly IBaseRepository<AddressEntity> _addressRepository;

        public UserService(IUnitOfWork unitOfWork, UserManager<UserEntity> userManager, IBaseRepository<ContactEntity> contactRepository, IBaseRepository<AddressEntity> addressRepository)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _contactRepository = contactRepository;
            _addressRepository = addressRepository;
        }

        public async Task<BusinessLogicResponse<UserEntity>> UpdateAsync(string id, UpdateUserDto dto)
        {
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(p => p.Id == id);

            if (existingUser == null)
            {
                return BusinessLogicResponse<UserEntity>.Failure(BusinessLogicResponseResult.ResourceDoesntExist, "User with given id not found");
            }
            
            var existingAddress = await _addressRepository.GetById(dto.AddressId).FirstOrDefaultAsync();
            var existingContact = await _contactRepository.GetById(dto.ContactId).FirstOrDefaultAsync();

            if (existingAddress == null)
            {
                return BusinessLogicResponse<UserEntity>.Failure(BusinessLogicResponseResult.ConflictOccured, "Address for fitness club creating not found");
            }

            existingUser.Address = existingAddress;
            
            if (existingContact == null)
            {
                return BusinessLogicResponse<UserEntity>.Failure(BusinessLogicResponseResult.ConflictOccured, "Contact for fitness club creating not found");
            }

            existingUser.Contact = existingContact;

            existingUser.FirstName = dto.FirstName;
            existingUser.LastName = dto.LastName;

            await _userManager.UpdateAsync(existingUser);
            await _unitOfWork.CommitTransactionsAsync();
            
            return BusinessLogicResponse<UserEntity>.Success(BusinessLogicResponseResult.Updated);
        }

        public async Task<BusinessLogicResponse<UserEntity>> DeleteAsync(string id)
        {
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(p => p.Id == id);

            if (existingUser == null)
            {
                return BusinessLogicResponse<UserEntity>.Failure(BusinessLogicResponseResult.ResourceDoesntExist, "User with given id not found");
            }

            await _userManager.DeleteAsync(existingUser);
            await _unitOfWork.CommitTransactionsAsync();
            
            return BusinessLogicResponse<UserEntity>.Success(BusinessLogicResponseResult.Deleted);
        }
    }
}