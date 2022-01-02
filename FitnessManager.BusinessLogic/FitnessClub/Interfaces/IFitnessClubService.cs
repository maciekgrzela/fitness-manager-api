using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.FitnessClub;

namespace FitnessManager.BusinessLogic.FitnessClub.Interfaces
{
    public interface IFitnessClubService
    {
        Task<IEnumerable<FitnessClubEntity>> GetAllAsync();
        Task<BusinessLogicResponse<FitnessClubEntity>> GetByIdAsync(Guid id);
        Task<BusinessLogicResponse<FitnessClubEntity>> SaveAsync(SaveFitnessClubDto dto);

        Task<BusinessLogicResponse<DepartmentEntity>> AssignDepartmentToFitnessClubAsync(Guid id,
            AssignDepartmentToFitnessClubDto dto);
        Task<BusinessLogicResponse<FitnessClubEntity>> UpdateAsync(Guid id, SaveFitnessClubDto dto);
        Task<BusinessLogicResponse<FitnessClubEntity>> DeleteAsync(Guid id);
    }
}