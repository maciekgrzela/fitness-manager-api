using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.FitnessClass;

namespace FitnessManager.BusinessLogic.FitnessClass.Interfaces
{
    public interface IFitnessClassService
    {
        Task<IEnumerable<FitnessClassEntity>> GetAllAsync();
        Task<BusinessLogicResponse<FitnessClassEntity>> GetByIdAsync(Guid id);
        Task<BusinessLogicResponse<FitnessClassEntity>> SaveAsync(SaveFitnessClassDto dto);
        Task<BusinessLogicResponse<FitnessClassEnrolmentsEntity>> SaveEnrolmentAsync(Guid id, SaveFitnessClassEnrolmentDto dto);
        Task<BusinessLogicResponse<FitnessClassEntity>> UpdateAsync(Guid id, SaveFitnessClassDto dto);
        Task<BusinessLogicResponse<FitnessClassEntity>> DeleteAsync(Guid id);
    }
}