using System;

namespace FitnessManager.DataAccess.Entities
{
    public class CustomerFitnessClassEnrolmentEntity : BaseEntity
    {
        public Guid EnrolmentsId { get; set; }
        public virtual FitnessClassEnrolmentsEntity Enrolments { get; set; }
        public Guid CustomerId { get; set; }
        public virtual CustomerEntity Customer { get; set; }
    }
}