using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.SportsEquipment;

namespace FitnessManager.BusinessLogic.SportsEquipment.Interfaces
{
    public interface ISportsEquipmentService
    {
        Task<IEnumerable<SportsEquipmentEntity>> GetAllAsync();
        Task<BusinessLogicResponse<SportsEquipmentEntity>> GetByIdAsync(Guid id);
        Task<BusinessLogicResponse<SportsEquipmentEntity>> SaveAsync(SaveSportsEquipmentDto dto);
        Task<BusinessLogicResponse<SportsEquipmentEntity>> UpdateAsync(Guid id, SaveSportsEquipmentDto dto);
        Task<BusinessLogicResponse<SportsEquipmentEntity>> DeleteAsync(Guid id);
    }
}