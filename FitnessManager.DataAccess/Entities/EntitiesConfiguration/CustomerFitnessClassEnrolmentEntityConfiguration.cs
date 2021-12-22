using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessManager.DataAccess.Entities.EntitiesConfiguration
{
    public class CustomerFitnessClassEnrolmentEntityConfiguration : IEntityTypeConfiguration<CustomerFitnessClassEnrolmentEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerFitnessClassEnrolmentEntity> builder)
        {
            builder.ToTable("CustomerClassesEnrolments");

            builder.HasKey(p => new {p.CustomerId, p.EnrolmentId});

            builder
                .HasOne(p => p.Enrolment)
                .WithMany(p => p.Customers)
                .HasForeignKey(p => p.EnrolmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(p => p.Customer)
                .WithMany(p => p.Enrolments)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}