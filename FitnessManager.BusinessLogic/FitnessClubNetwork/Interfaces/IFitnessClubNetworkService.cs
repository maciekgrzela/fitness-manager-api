using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.FitnessClubNetwork;

namespace FitnessManager.BusinessLogic.FitnessClubNetwork.Interfaces
{
    public interface IFitnessClubNetworkService
    {
        Task<IEnumerable<FitnessClubNetworkEntity>> GetAllAsync();
        Task<BusinessLogicResponse<FitnessClubNetworkEntity>> GetByIdAsync(Guid id);
        Task<BusinessLogicResponse<FitnessClubNetworkEntity>> SaveAsync(SaveFitnessClubNetworkDto dto);
        Task<BusinessLogicResponse<FitnessClubNetworkEntity>> UpdateAsync(Guid id, SaveFitnessClubNetworkDto dto);
        Task<BusinessLogicResponse<FitnessClubNetworkEntity>> DeleteAsync(Guid id);
    }
}