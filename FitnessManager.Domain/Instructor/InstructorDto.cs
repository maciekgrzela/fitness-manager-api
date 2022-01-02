using System.Collections.Generic;
using FitnessManager.Domain.Contact;

namespace FitnessManager.Domain.Instructor
{
    public class InstructorDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ContactDto Contact { get; set; }
        public virtual ICollection<FitnessClassForInstructorDto> ClassesAssignedAsDefaultInstructor { get; set; }
        public virtual ICollection<ClassEnrolmentsForInstructorDto> ClassEnrolments { get; set; }
    }
}