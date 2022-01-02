using System;
using System.Collections.Generic;

namespace FitnessManager.Domain.FitnessClass
{
    public class FitnessClassDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public int MaximumParticipants { get; set; }
        public Guid? DefaultInstructorId { get; set; }
        public virtual InstructorForFitnessClassDto DefaultInstructor { get; set; }
        public virtual ICollection<FitnessClassEnrolmentsForFitnessClassDto> Enrolments { get; set; }
    }
}