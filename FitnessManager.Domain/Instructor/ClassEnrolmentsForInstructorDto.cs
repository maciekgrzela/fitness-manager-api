using System;
using System.Collections.Generic;

namespace FitnessManager.Domain.Instructor
{
    public class ClassEnrolmentsForInstructorDto
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public  ICollection<CustomersForInstructorDto> Customers { get; set; }
    }
}