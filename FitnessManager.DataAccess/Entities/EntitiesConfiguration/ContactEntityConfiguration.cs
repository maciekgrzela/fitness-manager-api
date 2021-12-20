using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessManager.DataAccess.Entities.EntitiesConfiguration
{
    public class ContactEntityConfiguration : IEntityTypeConfiguration<ContactEntity>
    {
        public void Configure(EntityTypeBuilder<ContactEntity> builder)
        {
            builder.ToTable("Contacts");

            // With one-to-one relationship dependent entity is defined by type provided to HasForeignKey
            // OnDelete method meaning: "What should I do with dependent entity (this case AddressEntity) when principal entity (this case CustomerEntity) has been deleted
            builder
                .HasOne(p => p.Customer)
                .WithOne(p => p.Contact)
                .HasForeignKey<ContactEntity>(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder
                .HasOne(p => p.Department)
                .WithOne(p => p.Contact)
                .HasForeignKey<ContactEntity>(p => p.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(p => p.FitnessClub)
                .WithOne(p => p.BaseContact)
                .HasForeignKey<ContactEntity>(p => p.FitnessClubId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder
                .HasOne(p => p.User)
                .WithOne(p => p.Contact)
                .HasForeignKey<ContactEntity>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder
                .HasOne(p => p.Instructor)
                .WithOne(p => p.Contact)
                .HasForeignKey<ContactEntity>(p => p.InstructorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}