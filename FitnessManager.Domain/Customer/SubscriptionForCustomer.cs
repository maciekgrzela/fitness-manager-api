using System;

namespace FitnessManager.Domain.Customer
{
    public class SubscriptionForCustomer
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string PeriodType { get; set; }
        public double PricePerPeriod { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public string PaymentStatus { get; set; }
    }
}