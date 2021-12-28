using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Address.Interfaces;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.BusinessLogic.Common.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.DataAccess.Repositories.Interfaces;
using FitnessManager.Domain.Address;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.BusinessLogic.Address
{
    public class AddressService : IAddressService
    {
        private readonly IBaseRepository<AddressEntity> _baseAddressRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddressService(IBaseRepository<AddressEntity> baseAddressRepository, IUnitOfWork unitOfWork)
        {
            _baseAddressRepository = baseAddressRepository;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<IEnumerable<AddressEntity>> GetAllAsync()
        {
            return await _baseAddressRepository.GetAll()
                .Include(p => p.Customer)
                .Include(p => p.Department)
                .Include(p => p.User)
                .Include(p => p.FitnessClub)
                .ToListAsync();
        }

        public async Task<BusinessLogicResponse<AddressEntity>> GetByIdAsync(Guid id)
        {
            var address = await _baseAddressRepository.GetById(id);

            if (address == null)
            {
                return BusinessLogicResponse<AddressEntity>.Failure(BusinessLogicResponseResult.ResourceDoesntExist,
                    "There is no address for specified identifier");
            }
            
            return BusinessLogicResponse<AddressEntity>.Success(BusinessLogicResponseResult.Ok, address);
        }

        public async Task<BusinessLogicResponse<AddressEntity>> SaveAsync(SaveAddressDto dto)
        {
            var address = new AddressEntity
            {
                Id = Guid.NewGuid(),
                City = dto.City,
                Country = dto.Country,
                Number = dto.Number,
                Street = dto.Street,
                PostalCode = dto.PostalCode,
                User = null,
                FitnessClub = null,
                Customer = null,
                Department = null
            };

            var addressSaved = await _baseAddressRepository.Add(address);
            await _unitOfWork.CommitTransactionsAsync();
            
            return BusinessLogicResponse<AddressEntity>.Success(BusinessLogicResponseResult.Created, addressSaved);
        }

        public async Task<BusinessLogicResponse<AddressEntity>> UpdateAsync(Guid id, SaveAddressDto dto)
        {
            var existingAddress = await _baseAddressRepository.GetById(id);

            if (existingAddress == null)
            {
                return BusinessLogicResponse<AddressEntity>.Failure(BusinessLogicResponseResult.ResourceDoesntExist,
                    "There is no address for specified identifier");
            }

            existingAddress.City = dto.City;
            existingAddress.Country = dto.Country;
            existingAddress.Number = dto.Number;
            existingAddress.Street = dto.Street;
            existingAddress.PostalCode = dto.PostalCode;
            
            _baseAddressRepository.Update(existingAddress);
            await _unitOfWork.CommitTransactionsAsync();
            
            return BusinessLogicResponse<AddressEntity>.Success(BusinessLogicResponseResult.Updated, existingAddress);
        }

        public async Task<BusinessLogicResponse<AddressEntity>> DeleteAsync(Guid id)
        {
            var existingAddress = await _baseAddressRepository.GetById(id);

            if (existingAddress == null)
            {
                return BusinessLogicResponse<AddressEntity>.Failure(BusinessLogicResponseResult.ResourceDoesntExist,
                    "There is no address for specified identifier");
            }

            await _baseAddressRepository.Delete(existingAddress);
            await _unitOfWork.CommitTransactionsAsync();
            
            return BusinessLogicResponse<AddressEntity>.Success(BusinessLogicResponseResult.Deleted, new AddressEntity());
        }
    }
}