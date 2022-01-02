using System.Collections.Generic;
using AutoMapper;
using FitnessManager.DataAccess.Entities;
using FitnessManager.Domain.Contact;

namespace FitnessManager.Domain.Instructor
{
    public class InstructorDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public  ContactDto Contact { get; set; }
        public  ICollection<FitnessClassForInstructorDto> ClassesAssignedAsDefaultInstructor { get; set; }
        public  ICollection<ClassEnrolmentsForInstructorDto> ClassEnrolments { get; set; }
    }

    public class InstructorMapping : Profile
    {
        public InstructorMapping()
        {
            CreateMap<InstructorEntity, InstructorDto>();
            CreateMap<FitnessClassEntity, FitnessClassForInstructorDto>();
            CreateMap<FitnessClassEnrolmentsEntity, ClassEnrolmentsForInstructorDto>();
        }
    }
}