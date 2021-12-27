using System;

namespace FitnessManager.Domain.Customer
{
    public class FitnessClassForCustomer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public int MaximumParticipants { get; set; }
    }
}