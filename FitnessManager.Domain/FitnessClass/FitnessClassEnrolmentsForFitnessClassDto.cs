using System;
using System.Collections.Generic;
using FitnessManager.DataAccess.Entities;

namespace FitnessManager.Domain.FitnessClass
{
    public class FitnessClassEnrolmentsForFitnessClassDto
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public  InstructorEntity Instructor { get; set; }
        public  ICollection<CustomerForFitnessClassDto> Customers { get; set; }
    }
}