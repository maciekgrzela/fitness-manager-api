using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessManager.DataAccess.Entities
{
    public class FitnessClassEnrolmentsEntity : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid? InstructorId { get; set; }
        public virtual InstructorEntity Instructor { get; set; }
        public Guid FitnessClassId { get; set; }
        public virtual FitnessClassEntity FitnessClass { get; set; }
        public virtual ICollection<CustomerFitnessClassEnrolmentEntity> Customers { get; set; }
    }
}