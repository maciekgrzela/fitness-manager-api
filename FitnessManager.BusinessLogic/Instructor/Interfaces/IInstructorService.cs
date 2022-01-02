using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitnessManager.BusinessLogic.Common;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.Instructor;

namespace FitnessManager.BusinessLogic.Instructor.Interfaces
{
    public interface IInstructorService
    {
        Task<IEnumerable<InstructorEntity>> GetAllAsync();
        Task<BusinessLogicResponse<InstructorEntity>> GetByIdAsync(Guid id);
        Task<BusinessLogicResponse<InstructorEntity>> SaveAsync(SaveInstructorDto dto);
        Task<BusinessLogicResponse<InstructorEntity>> UpdateAsync(Guid id, SaveInstructorDto dto);
        Task<BusinessLogicResponse<InstructorEntity>> DeleteAsync(Guid id);
    }
}