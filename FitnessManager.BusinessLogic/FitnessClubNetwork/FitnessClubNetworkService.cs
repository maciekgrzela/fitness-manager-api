using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.BusinessLogic.Common.Interfaces;
using FitnessManager.BusinessLogic.FitnessClubNetwork.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.DataAccess.Repositories.Interfaces;
using FitnessManager.Domain.FitnessClubNetwork;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.BusinessLogic.FitnessClubNetwork
{
    public class FitnessClubNetworkService : IFitnessClubNetworkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<FitnessClubNetworkEntity> _fitnessClubNetworkRepository;

        public FitnessClubNetworkService(IUnitOfWork unitOfWork, IBaseRepository<FitnessClubNetworkEntity> fitnessClubNetworkRepository)
        {
            _unitOfWork = unitOfWork;
            _fitnessClubNetworkRepository = fitnessClubNetworkRepository;
        }
        
        public async Task<IEnumerable<FitnessClubNetworkEntity>> GetAllAsync()
        {
            return await _fitnessClubNetworkRepository.GetAll()
                .Include(p => p.FitnessClubs)
                .ToListAsync();
        }

        public async Task<BusinessLogicResponse<FitnessClubNetworkEntity>> GetByIdAsync(Guid id)
        {
            var existingFitnessClubNetwork = await _fitnessClubNetworkRepository.GetById(id)
                .Include(p => p.FitnessClubs)
                .FirstOrDefaultAsync();

            return existingFitnessClubNetwork == null
                ? BusinessLogicResponse<FitnessClubNetworkEntity>.Failure(BusinessLogicResponseResult.ResourceDoesntExist,
                    "FitnessClubNetwork with given id is not found")
                : BusinessLogicResponse<FitnessClubNetworkEntity>.Success(BusinessLogicResponseResult.Ok, existingFitnessClubNetwork);
        }

        public async Task<BusinessLogicResponse<FitnessClubNetworkEntity>> SaveAsync(SaveFitnessClubNetworkDto dto)
        {
            var fitnessClubNetwork = new FitnessClubNetworkEntity
            {
                Name = dto.Name,
                Description = dto.Description,
                LogoUrl = dto.LogoUrl
            };

            await _fitnessClubNetworkRepository.Add(fitnessClubNetwork);
            await _unitOfWork.CommitTransactionsAsync();
            
            return BusinessLogicResponse<FitnessClubNetworkEntity>.Success(BusinessLogicResponseResult.Created);
        }

        public async Task<BusinessLogicResponse<FitnessClubNetworkEntity>> UpdateAsync(Guid id, SaveFitnessClubNetworkDto dto)
        {
            var existingFitnessClubNetwork = await _fitnessClubNetworkRepository.GetById(id).FirstOrDefaultAsync();

            if (existingFitnessClubNetwork == null)
            {
                return BusinessLogicResponse<FitnessClubNetworkEntity>.Failure(
                    BusinessLogicResponseResult.ResourceDoesntExist, "FitnessClubNetwork with given id is not found");
            }

            existingFitnessClubNetwork.Name = dto.Name;
            existingFitnessClubNetwork.Description = dto.Description;
            existingFitnessClubNetwork.LogoUrl = dto.LogoUrl;
            
            _fitnessClubNetworkRepository.Update(existingFitnessClubNetwork);
            await _unitOfWork.CommitTransactionsAsync();
            
            return BusinessLogicResponse<FitnessClubNetworkEntity>.Success(BusinessLogicResponseResult.Updated);
        }

        public async Task<BusinessLogicResponse<FitnessClubNetworkEntity>> DeleteAsync(Guid id)
        {
            var existingFitnessClubNetwork = await _fitnessClubNetworkRepository.GetById(id).FirstOrDefaultAsync();

            if (existingFitnessClubNetwork == null)
            {
                return BusinessLogicResponse<FitnessClubNetworkEntity>.Failure(
                    BusinessLogicResponseResult.ResourceDoesntExist, "FitnessClubNetwork with given id is not found");
            }

            await _fitnessClubNetworkRepository.Delete(id);
            await _unitOfWork.CommitTransactionsAsync();

            return BusinessLogicResponse<FitnessClubNetworkEntity>.Success(BusinessLogicResponseResult.Deleted);
        }
    }
}