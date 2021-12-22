using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitnessManager.DataAccess.Entities
{
    public class SubscriptionEntity : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string PeriodType { get; set; }
        public double PricePerPeriod { get; set; }
        public virtual ICollection<CustomerSubscriptionsEntity> Customers { get; set; }
    }
}