using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.BusinessLogic.Common.Interfaces;
using FitnessManager.BusinessLogic.FitnessClub.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.DataAccess.Repositories.Interfaces;
using FitnessManager.Domain.FitnessClub;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.BusinessLogic.FitnessClub
{
    public class FitnessClubService : IFitnessClubService
    {
        private readonly IBaseRepository<FitnessClubEntity> _fitnessClubRepository;
        private readonly IBaseRepository<FitnessClubNetworkEntity> _fitnessClubNetworkRepository;
        private readonly IBaseRepository<AddressEntity> _addressRepository;
        private readonly IBaseRepository<ContactEntity> _contactRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FitnessClubService(IBaseRepository<FitnessClubEntity> fitnessClubRepository, IBaseRepository<FitnessClubNetworkEntity> fitnessClubNetworkRepository, IBaseRepository<AddressEntity> addressRepository, IBaseRepository<ContactEntity> contactRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _fitnessClubRepository = fitnessClubRepository;
            _fitnessClubNetworkRepository = fitnessClubNetworkRepository;
            _addressRepository = addressRepository;
            _contactRepository = contactRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FitnessClubEntity>> GetAllAsync()
        {
            return await _fitnessClubRepository.GetAll()
                .Include(p => p.BaseAddress)
                .Include(p => p.BaseContact)
                .Include(p => p.Departments)
                .Include(p => p.Halls)
                .Include(p => p.FitnessClubNetwork)
                .ToListAsync();
        }

        public async Task<BusinessLogicResponse<FitnessClubEntity>> GetByIdAsync(Guid id)
        {
            var existingFitnessClub = await _fitnessClubRepository.GetById(id)
                .Include(p => p.BaseAddress)
                .Include(p => p.BaseContact)
                .Include(p => p.Departments)
                .Include(p => p.Halls)
                .Include(p => p.FitnessClubNetwork)
                .FirstOrDefaultAsync();

            return existingFitnessClub == null
                ? BusinessLogicResponse<FitnessClubEntity>.Failure(BusinessLogicResponseResult.ResourceDoesntExist,
                    "FitnessClub with given id is not found")
                : BusinessLogicResponse<FitnessClubEntity>.Success(BusinessLogicResponseResult.Ok, existingFitnessClub);
        }

        public async Task<BusinessLogicResponse<FitnessClubEntity>> SaveAsync(SaveFitnessClubDto dto)
        {
            var fitnessClub = new FitnessClubEntity();

            if (dto.FitnessClubNetworkId != null)
            {
                var existingNetwork = await _fitnessClubNetworkRepository.GetById(dto.FitnessClubNetworkId.Value)
                    .FirstOrDefaultAsync();

                if (existingNetwork == null)
                {
                    return BusinessLogicResponse<FitnessClubEntity>.Failure(BusinessLogicResponseResult.ConflictOccured, "Fitness Club Network for fitness club creating not found");
                }

                fitnessClub.FitnessClubNetwork = existingNetwork;
            }

            var existingAddress = await _addressRepository.GetById(dto.BaseAddressId).FirstOrDefaultAsync();
            var existingContact = await _contactRepository.GetById(dto.BaseContactId).FirstOrDefaultAsync();

            if (existingAddress == null)
            {
                return BusinessLogicResponse<FitnessClubEntity>.Failure(BusinessLogicResponseResult.ConflictOccured, "Address for fitness club creating not found");
            }

            fitnessClub.BaseAddress = existingAddress;
            
            if (existingContact == null)
            {
                return BusinessLogicResponse<FitnessClubEntity>.Failure(BusinessLogicResponseResult.ConflictOccured, "Contact for fitness club creating not found");
            }

            fitnessClub.BaseContact = existingContact;

            fitnessClub.Name = dto.Name;
            fitnessClub.Departments = new List<DepartmentEntity>();
            fitnessClub.Halls = new List<HallEntity>();


            await _fitnessClubRepository.Add(fitnessClub);
            await _unitOfWork.CommitTransactionsAsync();
            
            return BusinessLogicResponse<FitnessClubEntity>.Success(BusinessLogicResponseResult.Created);
        }

        public async Task<BusinessLogicResponse<FitnessClubEntity>> UpdateAsync(Guid id, SaveFitnessClubDto dto)
        {
            var fitnessClub = new FitnessClubEntity();

            if (dto.FitnessClubNetworkId != null)
            {
                var existingNetwork = await _fitnessClubNetworkRepository.GetById(dto.FitnessClubNetworkId.Value)
                    .FirstOrDefaultAsync();

                if (existingNetwork == null)
                {
                    return BusinessLogicResponse<FitnessClubEntity>.Failure(BusinessLogicResponseResult.ConflictOccured, "Fitness Club Network for fitness club creating not found");
                }

                fitnessClub.FitnessClubNetwork = existingNetwork;
            }

            var existingAddress = await _addressRepository.GetById(dto.BaseAddressId).FirstOrDefaultAsync();
            var existingContact = await _contactRepository.GetById(dto.BaseContactId).FirstOrDefaultAsync();

            if (existingAddress == null)
            {
                return BusinessLogicResponse<FitnessClubEntity>.Failure(BusinessLogicResponseResult.ConflictOccured, "Address for fitness club creating not found");
            }

            fitnessClub.BaseAddress = existingAddress;
            
            if (existingContact == null)
            {
                return BusinessLogicResponse<FitnessClubEntity>.Failure(BusinessLogicResponseResult.ConflictOccured, "Contact for fitness club creating not found");
            }

            fitnessClub.BaseContact = existingContact;

            fitnessClub.Name ??= dto.Name;

            _fitnessClubRepository.Update(fitnessClub);
            await _unitOfWork.CommitTransactionsAsync();
            
            return BusinessLogicResponse<FitnessClubEntity>.Success(BusinessLogicResponseResult.Updated);
        }

        public async Task<BusinessLogicResponse<FitnessClubEntity>> DeleteAsync(Guid id)
        {
            var existingFitnessClub = await _fitnessClubRepository.GetById(id).FirstOrDefaultAsync();

            if (existingFitnessClub == null)
            {
                return BusinessLogicResponse<FitnessClubEntity>.Failure(
                    BusinessLogicResponseResult.ResourceDoesntExist, "FitnessClub with given id is not found");
            }

            await _fitnessClubRepository.Delete(id);
            await _unitOfWork.CommitTransactionsAsync();

            return BusinessLogicResponse<FitnessClubEntity>.Success(BusinessLogicResponseResult.Deleted);
        }
    }
}