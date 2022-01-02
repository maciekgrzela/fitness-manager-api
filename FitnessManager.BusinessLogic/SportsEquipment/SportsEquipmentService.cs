using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.BusinessLogic.Common.Interfaces;
using FitnessManager.BusinessLogic.SportsEquipment.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.DataAccess.Repositories.Interfaces;
using FitnessManager.Domain.SportsEquipment;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.BusinessLogic.SportsEquipment
{
    public class SportsEquipmentService : ISportsEquipmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<SportsEquipmentEntity> _sportsEquipmentRepository;
        private readonly IBaseRepository<HallEntity> _hallRepository;

        public SportsEquipmentService(IUnitOfWork unitOfWork, IBaseRepository<SportsEquipmentEntity> sportsEquipmentRepository, IBaseRepository<HallEntity> hallRepository)
        {
            _unitOfWork = unitOfWork;
            _sportsEquipmentRepository = sportsEquipmentRepository;
            _hallRepository = hallRepository;
        }
        
        public async Task<IEnumerable<SportsEquipmentEntity>> GetAllAsync()
        {
            return await _sportsEquipmentRepository.GetAll()
                .Include(p => p.Hall)
                .Include(p => p.EquipmentReservations)
                .ToListAsync();
        }

        public async Task<BusinessLogicResponse<SportsEquipmentEntity>> GetByIdAsync(Guid id)
        {
            var existingSportsEquipment = await _sportsEquipmentRepository.GetById(id)
                .Include(p => p.Hall)
                .Include(p => p.EquipmentReservations)
                .FirstOrDefaultAsync();

            return existingSportsEquipment == null
                ? BusinessLogicResponse<SportsEquipmentEntity>.Failure(BusinessLogicResponseResult.ResourceDoesntExist,
                    "SportsEquipment with given id is not found")
                : BusinessLogicResponse<SportsEquipmentEntity>.Success(BusinessLogicResponseResult.Ok, existingSportsEquipment);
        }

        public async Task<BusinessLogicResponse<SportsEquipmentEntity>> SaveAsync(SaveSportsEquipmentDto dto)
        {
            var existingHall = await _hallRepository.GetById(dto.HallId).FirstOrDefaultAsync();

            if (existingHall == null)
            {
                return BusinessLogicResponse<SportsEquipmentEntity>.Failure(BusinessLogicResponseResult.ConflictOccured, "Hall for Sports Equipment creating not found");
            }

            var equipment = new SportsEquipmentEntity
            {
                Name = dto.Name,
                Type = dto.Type,
                SerialNumber = dto.SerialNumber,
                Hall = existingHall,
                EquipmentReservations = new List<EquipmentReservationEntity>()
            };

            await _sportsEquipmentRepository.Add(equipment);
            await _unitOfWork.CommitTransactionsAsync();
            
            return BusinessLogicResponse<SportsEquipmentEntity>.Success(BusinessLogicResponseResult.Created);
        }

        public async Task<BusinessLogicResponse<SportsEquipmentEntity>> UpdateAsync(Guid id, SaveSportsEquipmentDto dto)
        {
            var existingHall = await _hallRepository.GetById(dto.HallId).FirstOrDefaultAsync();

            if (existingHall == null)
            {
                return BusinessLogicResponse<SportsEquipmentEntity>.Failure(BusinessLogicResponseResult.ConflictOccured, "Hall for Sports Equipment creating not found");
            }
            
            var existingSportsEquipment = await _sportsEquipmentRepository.GetById(id).FirstOrDefaultAsync();

            if (existingSportsEquipment == null)
            {
                return BusinessLogicResponse<SportsEquipmentEntity>.Failure(
                    BusinessLogicResponseResult.ResourceDoesntExist, "SportsEquipment with given id is not found");
            }

            existingSportsEquipment.Name = dto.Name;
            existingSportsEquipment.Type = dto.Type;
            existingSportsEquipment.SerialNumber = dto.SerialNumber;
            existingSportsEquipment.Hall = existingHall;

            _sportsEquipmentRepository.Update(existingSportsEquipment);
            await _unitOfWork.CommitTransactionsAsync();
            
            return BusinessLogicResponse<SportsEquipmentEntity>.Success(BusinessLogicResponseResult.Updated);
        }

        public async Task<BusinessLogicResponse<SportsEquipmentEntity>> DeleteAsync(Guid id)
        {
            var existingSportsEquipment = await _sportsEquipmentRepository.GetById(id).FirstOrDefaultAsync();

            if (existingSportsEquipment == null)
            {
                return BusinessLogicResponse<SportsEquipmentEntity>.Failure(
                    BusinessLogicResponseResult.ResourceDoesntExist, "SportsEquipment with given id is not found");
            }

            await _sportsEquipmentRepository.Delete(id);
            await _unitOfWork.CommitTransactionsAsync();

            return BusinessLogicResponse<SportsEquipmentEntity>.Success(BusinessLogicResponseResult.Deleted);
        }
    }
}