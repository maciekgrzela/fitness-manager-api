using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.BusinessLogic.Common.Interfaces;
using FitnessManager.BusinessLogic.Hall.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.DataAccess.Repositories.Interfaces;
using FitnessManager.Domain.Hall;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.BusinessLogic.Hall
{
    public class HallService : IHallService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<HallEntity> _hallRepository;
        private readonly IBaseRepository<FitnessClubEntity> _fitnessClubRepository;

        public HallService(IUnitOfWork unitOfWork, IBaseRepository<HallEntity> hallRepository, IBaseRepository<FitnessClubEntity> fitnessClubRepository)
        {
            _unitOfWork = unitOfWork;
            _hallRepository = hallRepository;
            _fitnessClubRepository = fitnessClubRepository;
        }
        
        public async Task<IEnumerable<HallEntity>> GetAllAsync()
        {
            return await _hallRepository.GetAll()
                .Include(p => p.SportsEquipments)
                .Include(p => p.FitnessClub)
                .ToListAsync();
        }

        public async Task<BusinessLogicResponse<HallEntity>> GetByIdAsync(Guid id)
        {
            var existingHall = await _hallRepository.GetById(id)
                .Include(p => p.SportsEquipments)
                .Include(p => p.FitnessClub)
                .FirstOrDefaultAsync();

            return existingHall == null
                ? BusinessLogicResponse<HallEntity>.Failure(BusinessLogicResponseResult.ResourceDoesntExist,
                    "Hall with given id is not found")
                : BusinessLogicResponse<HallEntity>.Success(BusinessLogicResponseResult.Ok, existingHall);
        }

        public async Task<BusinessLogicResponse<HallEntity>> SaveAsync(SaveHallDto dto)
        {
            var existingFitnessClub = await _fitnessClubRepository.GetById(dto.FitnessClubId).FirstOrDefaultAsync();

            if (existingFitnessClub == null)
            {
                return BusinessLogicResponse<HallEntity>.Failure(BusinessLogicResponseResult.ConflictOccured, "Fitness Club for Hall creating not found");
            }

            var hall = new HallEntity
            {
                MaximumCapacity = dto.MaximumCapacity,
                FitnessClub = existingFitnessClub,
                SportsEquipments = new List<SportsEquipmentEntity>()
            };

            await _hallRepository.Add(hall);
            await _unitOfWork.CommitTransactionsAsync();
            
            return BusinessLogicResponse<HallEntity>.Success(BusinessLogicResponseResult.Created);
        }

        public async Task<BusinessLogicResponse<HallEntity>> UpdateAsync(Guid id, SaveHallDto dto)
        {
            var existingFitnessClub = await _fitnessClubRepository.GetById(dto.FitnessClubId).FirstOrDefaultAsync();

            if (existingFitnessClub == null)
            {
                return BusinessLogicResponse<HallEntity>.Failure(BusinessLogicResponseResult.ConflictOccured, "Fitness Club for Hall creating not found");
            }
            
            var existingHall = await _hallRepository.GetById(id).FirstOrDefaultAsync();

            if (existingHall == null)
            {
                return BusinessLogicResponse<HallEntity>.Failure(
                    BusinessLogicResponseResult.ResourceDoesntExist, "Hall with given id is not found");
            }

            existingHall.MaximumCapacity = dto.MaximumCapacity;
            existingHall.FitnessClub = existingFitnessClub;

            _hallRepository.Update(existingHall);
            await _unitOfWork.CommitTransactionsAsync();
            
            return BusinessLogicResponse<HallEntity>.Success(BusinessLogicResponseResult.Updated);
        }

        public async Task<BusinessLogicResponse<HallEntity>> DeleteAsync(Guid id)
        {
            var existingHall = await _hallRepository.GetById(id).FirstOrDefaultAsync();

            if (existingHall == null)
            {
                return BusinessLogicResponse<HallEntity>.Failure(
                    BusinessLogicResponseResult.ResourceDoesntExist, "Hall with given id is not found");
            }

            await _hallRepository.Delete(id);
            await _unitOfWork.CommitTransactionsAsync();

            return BusinessLogicResponse<HallEntity>.Success(BusinessLogicResponseResult.Deleted);
        }
    }
}