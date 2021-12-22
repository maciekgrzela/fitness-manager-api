using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessManager.DataAccess.Entities.EntitiesConfiguration
{
    public class AddressEntityConfiguration : IEntityTypeConfiguration<AddressEntity>
    {
        public void Configure(EntityTypeBuilder<AddressEntity> builder)
        {
            builder.ToTable("Addresses");

            // With one-to-one relationship dependent entity is defined by type provided to HasForeignKey
            // OnDelete method meaning: "What should I do with dependent entity (this case AddressEntity) when principal entity (this case CustomerEntity) has been deleted
            builder
                .HasOne(p => p.Customer)
                .WithOne(p => p.Address)
                .HasForeignKey<AddressEntity>(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder
                .HasOne(p => p.Department)
                .WithOne(p => p.Address)
                .HasForeignKey<AddressEntity>(p => p.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(p => p.FitnessClub)
                .WithOne(p => p.BaseAddress)
                .HasForeignKey<AddressEntity>(p => p.FitnessClubId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder
                .HasOne(p => p.User)
                .WithOne(p => p.Address)
                .HasForeignKey<AddressEntity>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}