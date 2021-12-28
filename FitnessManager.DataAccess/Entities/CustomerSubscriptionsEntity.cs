using System;

namespace FitnessManager.DataAccess.Entities
{
    public class CustomerSubscriptionsEntity : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public CustomerEntity Customer { get; set; }
        public Guid SubscriptionId { get; set; }
        public SubscriptionEntity Subscription { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public string PaymentStatus { get; set; }
    }
}