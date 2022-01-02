using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.BusinessLogic.Common.Interfaces;
using FitnessManager.BusinessLogic.Instructor.Interfaces;
using FitnessManager.DataAccess.Entities;
using FitnessManager.DataAccess.Repositories.Interfaces;
using FitnessManager.Domain.Instructor;
using Microsoft.EntityFrameworkCore;

namespace FitnessManager.BusinessLogic.Instructor
{
    public class InstructorService : IInstructorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<InstructorEntity> _instructorRepository;
        private readonly IBaseRepository<ContactEntity> _contactRepository;

        public InstructorService(IUnitOfWork unitOfWork, IBaseRepository<InstructorEntity> instructorRepository, IBaseRepository<ContactEntity> contactRepository)
        {
            _unitOfWork = unitOfWork;
            _instructorRepository = instructorRepository;
            _contactRepository = contactRepository;
        }
        
        public async Task<IEnumerable<InstructorEntity>> GetAllAsync()
        {
            return await _instructorRepository.GetAll()
                .Include(p => p.Contact)
                .Include(p => p.ClassEnrolments)
                .Include(p => p.ClassesAssignedAsDefaultInstructor)
                .ToListAsync();
        }

        public async Task<BusinessLogicResponse<InstructorEntity>> GetByIdAsync(Guid id)
        {
            var existingInstructor = await _instructorRepository.GetById(id)
                .Include(p => p.Contact)
                .Include(p => p.ClassEnrolments)
                .Include(p => p.ClassesAssignedAsDefaultInstructor)
                .FirstOrDefaultAsync();

            return existingInstructor == null
                ? BusinessLogicResponse<InstructorEntity>.Failure(BusinessLogicResponseResult.ResourceDoesntExist,
                    "Instructor with given id is not found")
                : BusinessLogicResponse<InstructorEntity>.Success(BusinessLogicResponseResult.Ok, existingInstructor);
        }

        public async Task<BusinessLogicResponse<InstructorEntity>> SaveAsync(SaveInstructorDto dto)
        {
            var existingContact = await _contactRepository.GetById(dto.ContactId).FirstOrDefaultAsync();

            if (existingContact == null)
            {
                return BusinessLogicResponse<InstructorEntity>.Failure(BusinessLogicResponseResult.ConflictOccured, "Contact for instructor creating not found");
            }

            var instructor = new InstructorEntity
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Contact = existingContact,
                ClassEnrolments = new List<FitnessClassEnrolmentsEntity>(),
                ClassesAssignedAsDefaultInstructor = new List<FitnessClassEntity>()
            };

            await _instructorRepository.Add(instructor);
            await _unitOfWork.CommitTransactionsAsync();
            
            return BusinessLogicResponse<InstructorEntity>.Success(BusinessLogicResponseResult.Created);
        }

        public async Task<BusinessLogicResponse<InstructorEntity>> UpdateAsync(Guid id, SaveInstructorDto dto)
        {
            var existingContact = await _contactRepository.GetById(dto.ContactId).FirstOrDefaultAsync();

            if (existingContact == null)
            {
                return BusinessLogicResponse<InstructorEntity>.Failure(BusinessLogicResponseResult.ConflictOccured, "Contact for instructor creating not found");
            }
            
            var existingInstructor = await _instructorRepository.GetById(id).FirstOrDefaultAsync();

            if (existingInstructor == null)
            {
                return BusinessLogicResponse<InstructorEntity>.Failure(
                    BusinessLogicResponseResult.ResourceDoesntExist, "Instructor with given id is not found");
            }

            existingInstructor.FirstName = dto.FirstName;
            existingInstructor.LastName = dto.LastName;
            existingInstructor.Contact = existingContact;

            _instructorRepository.Update(existingInstructor);
            await _unitOfWork.CommitTransactionsAsync();
            
            return BusinessLogicResponse<InstructorEntity>.Success(BusinessLogicResponseResult.Updated);
        }

        public async Task<BusinessLogicResponse<InstructorEntity>> DeleteAsync(Guid id)
        {
            var existingInstructor = await _instructorRepository.GetById(id).FirstOrDefaultAsync();

            if (existingInstructor == null)
            {
                return BusinessLogicResponse<InstructorEntity>.Failure(
                    BusinessLogicResponseResult.ResourceDoesntExist, "Instructor with given id is not found");
            }

            await _instructorRepository.Delete(id);
            await _unitOfWork.CommitTransactionsAsync();

            return BusinessLogicResponse<InstructorEntity>.Success(BusinessLogicResponseResult.Deleted);
        }
    }
}