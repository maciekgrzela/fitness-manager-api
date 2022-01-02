using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.Address;

namespace FitnessManager.BusinessLogic.Address.Interfaces
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressEntity>> GetAllAsync();
        Task<BusinessLogicResponse<AddressEntity>> GetByIdAsync(Guid id);
        Task<BusinessLogicResponse<AddressEntity>> SaveAsync(SaveAddressDto dto);
        Task<BusinessLogicResponse<AddressEntity>> UpdateAsync(Guid id, SaveAddressDto dto);
        Task<BusinessLogicResponse<AddressEntity>> DeleteAsync(Guid id);
    }
}