using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.BusinessLogic.Contact.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.BusinessLogic.Contact
{
    public class ContactService : IContactService
    {
        private readonly IBaseRepository<ContactEntity> _baseContactRepository;

        public ContactService(IBaseRepository<ContactEntity> baseContactRepository)
        {
            _baseContactRepository = baseContactRepository;
        }
        
        public async Task<IEnumerable<ContactEntity>> GetAllAsync()
        {
            return await _baseContactRepository.GetAll()
                .Include(p => p.Customer)
                .Include(p => p.Department)
                .Include(p => p.User)
                .Include(p => p.FitnessClub)
                .Include(p => p.Instructor)
                .ToListAsync();
        }

        public async Task<BusinessLogicResponse<ContactEntity>> GetByIdAsync(Guid id)
        {
            var contact = await _baseContactRepository.GetById(id).FirstOrDefaultAsync();

            if (contact == null)
            {
                return BusinessLogicResponse<ContactEntity>.Failure(BusinessLogicResponseResult.ResourceDoesntExist,
                    "There is no contact for specified identifier");
            }
            
            return BusinessLogicResponse<ContactEntity>.Success(BusinessLogicResponseResult.Ok, contact);
        }
    }
}