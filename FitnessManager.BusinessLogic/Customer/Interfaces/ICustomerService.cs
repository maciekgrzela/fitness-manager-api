using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.Address;
using FitnessManager.Domain.Customer;

namespace FitnessManager.BusinessLogic.Customer.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerEntity>> GetAllAsync();
        Task<BusinessLogicResponse<CustomerEntity>> GetByIdAsync(Guid id);
        Task<BusinessLogicResponse<CustomerEntity>> SaveAsync(SaveCustomerDto dto);
        Task<BusinessLogicResponse<CustomerEntity>> UpdateAsync(Guid id, UpdateCustomerDto dto);
        Task<BusinessLogicResponse<CustomerEntity>> DeleteAsync(Guid id);
    }
}