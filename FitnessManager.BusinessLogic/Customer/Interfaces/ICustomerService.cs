using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.Customer;

namespace FitnessManager.BusinessLogic.Customer.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerEntity>> GetAllAsync();
        Task<BusinessLogicResponse<CustomerEntity>> GetByIdAsync(Guid id);
        Task<BusinessLogicResponse<CustomerEntity>> SaveAsync(SaveCustomerDto dto);
        Task<BusinessLogicResponse<CustomerEntity>> EnrolCustomerToFitnessClassAsync(Guid customerId, Guid enrolmentId);
        Task<BusinessLogicResponse<CustomerEntity>> DeleteCustomerToFitnessClassEnrolmentAsync(Guid customerId, Guid enrolmentId);
        Task<BusinessLogicResponse<CustomerEntity>> UpdateAsync(Guid id, UpdateCustomerDto dto);
        Task<BusinessLogicResponse<CustomerEntity>> DeleteAsync(Guid id);
    }
}