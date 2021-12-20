using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessManager.DataAccess.Entities.EntitiesConfiguration
{
    public class CustomerSubscriptionsEntityConfiguration : IEntityTypeConfiguration<CustomerSubscriptionsEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerSubscriptionsEntity> builder)
        {
            builder.HasKey(p => new {p.CustomerId, p.SubscriptionId});
            
            builder
                .HasOne(p => p.Customer)
                .WithMany(p => p.ActiveSubscriptions)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(p => p.Subscription)
                .WithMany(p => p.Customers)
                .HasForeignKey(p => p.SubscriptionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}