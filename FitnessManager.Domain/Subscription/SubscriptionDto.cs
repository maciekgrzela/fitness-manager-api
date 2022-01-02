using System.Collections.Generic;

namespace FitnessManager.Domain.Subscription
{
    public class SubscriptionDto
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string PeriodType { get; set; }
        public double PricePerPeriod { get; set; }
        public virtual ICollection<CustomerForSubscriptionDto> Customers { get; set; }
    }
}