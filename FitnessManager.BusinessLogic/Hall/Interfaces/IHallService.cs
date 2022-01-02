using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.Hall;

namespace FitnessManager.BusinessLogic.Hall.Interfaces
{
    public interface IHallService
    {
        Task<IEnumerable<HallEntity>> GetAllAsync();
        Task<BusinessLogicResponse<HallEntity>> GetByIdAsync(Guid id);
        Task<BusinessLogicResponse<HallEntity>> SaveAsync(SaveHallDto dto);
        Task<BusinessLogicResponse<HallEntity>> UpdateAsync(Guid id, SaveHallDto dto);
        Task<BusinessLogicResponse<HallEntity>> DeleteAsync(Guid id);
    }
}