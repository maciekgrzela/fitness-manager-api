using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.BusinessLogic.Common.Interfaces;
using FitnessManager.BusinessLogic.FitnessClass.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.DataAccess.Repositories.Interfaces;
using FitnessManager.Domain.FitnessClass;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.BusinessLogic.FitnessClass
{
    public class FitnessClassService : IFitnessClassService
    {
        private readonly IBaseRepository<FitnessClassEntity> _fitnessClassRepository;
        private readonly IBaseRepository<InstructorEntity> _instructorRepository;
        private readonly IBaseRepository<FitnessClassEnrolmentsEntity> _fitnessClassEnrolmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FitnessClassService(IBaseRepository<FitnessClassEntity> fitnessClassRepository, IBaseRepository<InstructorEntity> instructorRepository, IBaseRepository<FitnessClassEnrolmentsEntity> fitnessClassEnrolmentRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _fitnessClassRepository = fitnessClassRepository;
            _instructorRepository = instructorRepository;
            _fitnessClassEnrolmentRepository = fitnessClassEnrolmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FitnessClassEntity>> GetAllAsync()
        {
            return await _fitnessClassRepository.GetAll()
                .Include(p => p.DefaultInstructor)
                .ThenInclude(p => p.Contact)
                .Include(p => p.Enrolments)
                .ThenInclude(p => p.Customers)
                .ToListAsync();
        }

        public async Task<BusinessLogicResponse<FitnessClassEntity>> GetByIdAsync(Guid id)
        {
            var existingFitnessClass = await _fitnessClassRepository.GetById(id)
                .Include(p => p.DefaultInstructor)
                .ThenInclude(p => p.Contact)
                .Include(p => p.Enrolments)
                .ThenInclude(p => p.Customers)
                .FirstOrDefaultAsync();

            return existingFitnessClass == null ? BusinessLogicResponse<FitnessClassEntity>.Failure(BusinessLogicResponseResult.ResourceDoesntExist, "Fitness Class with given id is not found") : BusinessLogicResponse<FitnessClassEntity>.Success(BusinessLogicResponseResult.Ok, existingFitnessClass);
        }

        public async Task<BusinessLogicResponse<FitnessClassEntity>> SaveAsync(SaveFitnessClassDto dto)
        {
            var fitnessClass = new FitnessClassEntity();
            
            if (dto.DefaultInstructorId != null)
            {
                var existingInstructor = await _instructorRepository.GetById(dto.DefaultInstructorId.Value).FirstOrDefaultAsync();

                if (existingInstructor == null)
                {
                    return BusinessLogicResponse<FitnessClassEntity>.Failure(BusinessLogicResponseResult.ConflictOccured, "Instructor for fitness classes created not found");
                }

                fitnessClass.DefaultInstructor = existingInstructor;
            }

            fitnessClass.Name = dto.Name;
            fitnessClass.Price = dto.Price;
            fitnessClass.MaximumParticipants = dto.MaximumParticipants;
            fitnessClass.Type = dto.Type;
            fitnessClass.Enrolments = new List<FitnessClassEnrolmentsEntity>();

            await _fitnessClassRepository.Add(fitnessClass);
            await _unitOfWork.CommitTransactionsAsync();
            
            return BusinessLogicResponse<FitnessClassEntity>.Success(BusinessLogicResponseResult.Created);
        }

        public async Task<BusinessLogicResponse<FitnessClassEnrolmentsEntity>> SaveEnrolmentAsync(Guid id, SaveFitnessClassEnrolmentDto dto)
        {
            var enrolment = new FitnessClassEnrolmentsEntity();
            
            var existingFitnessClass = await _fitnessClassRepository.GetById(id).FirstOrDefaultAsync();

            if (existingFitnessClass == null)
            {
                return BusinessLogicResponse<FitnessClassEnrolmentsEntity>.Failure(BusinessLogicResponseResult.ConflictOccured, "FitnessClass with given id is not found");
            }

            enrolment.FitnessClass = existingFitnessClass;

            if (dto.InstructorId != null)
            {
                var existingInstructor = await _instructorRepository.GetById(dto.InstructorId.Value).FirstOrDefaultAsync();
                
                if (existingInstructor == null)
                {
                    return BusinessLogicResponse<FitnessClassEnrolmentsEntity>.Failure(BusinessLogicResponseResult.ConflictOccured, "Instructor with given id is not found");
                }

                enrolment.Instructor = existingInstructor;
            }

            enrolment.To = dto.To;
            enrolment.From = dto.From;
            enrolment.Customers = new List<CustomerFitnessClassEnrolmentEntity>();

            await _fitnessClassEnrolmentRepository.Add(enrolment);
            await _unitOfWork.CommitTransactionsAsync();
            
            return BusinessLogicResponse<FitnessClassEnrolmentsEntity>.Success(BusinessLogicResponseResult.Ok, enrolment);

        }

        public async Task<BusinessLogicResponse<FitnessClassEntity>> UpdateAsync(Guid id, SaveFitnessClassDto dto)
        {
            var existingFitnessClass = await _fitnessClassRepository.GetById(id).FirstOrDefaultAsync();

            if (existingFitnessClass == null)
            {
                return BusinessLogicResponse<FitnessClassEntity>.Failure(
                    BusinessLogicResponseResult.ResourceDoesntExist,
                    "FitnessClass with given id is not found");
            }
            
            existingFitnessClass.Name ??= dto.Name;
            existingFitnessClass.Price = dto.Price;
            existingFitnessClass.MaximumParticipants = dto.MaximumParticipants;
            existingFitnessClass.Type ??= dto.Type;

            _fitnessClassRepository.Update(existingFitnessClass);
            await _unitOfWork.CommitTransactionsAsync();
            
            return BusinessLogicResponse<FitnessClassEntity>.Success(BusinessLogicResponseResult.Updated);
        }

        public async Task<BusinessLogicResponse<FitnessClassEntity>> DeleteAsync(Guid id)
        {
            var existingFitnessClass = await _fitnessClassRepository.GetById(id)
                .FirstOrDefaultAsync();

            if (existingFitnessClass == null)
            {
                return BusinessLogicResponse<FitnessClassEntity>.Failure(
                    BusinessLogicResponseResult.ResourceDoesntExist, "Fitness Class with given id is not found");
            }

            await _fitnessClassRepository.Delete(id);
            await _unitOfWork.CommitTransactionsAsync();

            return BusinessLogicResponse<FitnessClassEntity>.Success(BusinessLogicResponseResult.Deleted);
        }
    }
}