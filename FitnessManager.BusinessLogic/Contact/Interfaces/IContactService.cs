using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.DataAccess.Entities;

namespace FitnessManager.BusinessLogic.Contact.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<ContactEntity>> GetAllAsync();
        Task<BusinessLogicResponse<ContactEntity>> GetByIdAsync(Guid id);
    }
}