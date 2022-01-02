using System;

namespace FitnessManager.Domain.Instructor
{
    public class SaveInstructorDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid ContactId { get; set; }
    }
}