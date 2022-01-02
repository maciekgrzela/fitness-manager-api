using System;

namespace FitnessManager.Domain.Customer
{
    public class FitnessClassEnrolmentsForCustomer
    {
        public Guid EnrolmentId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public InstructorForCustomer Instructor { get; set; }
        public Guid FitnessClassId { get; set; }
        public FitnessClassForCustomer FitnessClass { get; set; }
    }
}