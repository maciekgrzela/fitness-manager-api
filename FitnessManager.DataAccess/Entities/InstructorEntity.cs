using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessManager.DataAccess.Entities
{
    public class InstructorEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid ContactId { get; set; }
        public virtual ContactEntity Contact { get; set; }
        public virtual ICollection<FitnessClassEntity> ClassesAssignedAsDefaultInstructor { get; set; }
        public virtual ICollection<FitnessClassEnrolmentsEntity> ClassEnrolments { get; set; }
    }
}