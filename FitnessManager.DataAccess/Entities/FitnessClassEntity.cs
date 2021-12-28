using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessManager.DataAccess.Entities
{
    public class FitnessClassEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public int MaximumParticipants { get; set; }
        public Guid? DefaultInstructorId { get; set; }
        public virtual InstructorEntity DefaultInstructor { get; set; }
        public virtual ICollection<FitnessClassEnrolmentsEntity> Enrolments { get; set; }
    }
}