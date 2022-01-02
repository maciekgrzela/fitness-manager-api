using System.Collections.Generic;

namespace FitnessManager.DataAccess.Entities
{
    public class SubscriptionEntity : BaseEntity
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string PeriodType { get; set; }
        public double PricePerPeriod { get; set; }
        public virtual ICollection<CustomerSubscriptionsEntity> Customers { get; set; }
    }
}