using System;

namespace FitnessManager.Domain.FitnessClass
{
    public class SaveFitnessClassEnrolmentDto
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid? InstructorId { get; set; }
    }
}