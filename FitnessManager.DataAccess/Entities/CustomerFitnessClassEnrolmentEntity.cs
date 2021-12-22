using System;

namespace FitnessManager.DataAccess.Entities
{
    public class CustomerFitnessClassEnrolmentEntity : BaseEntity
    {
        public Guid EnrolmentId { get; set; }
        public virtual FitnessClassEnrolmentsEntity Enrolment { get; set; }
        public Guid CustomerId { get; set; }
        public virtual CustomerEntity Customer { get; set; }
    }
}