using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessManager.DataAccess.Entities.EntitiesConfiguration
{
    public class FitnessClassEnrolmentsEntityConfiguration : IEntityTypeConfiguration<FitnessClassEnrolmentsEntity>
    {
        public void Configure(EntityTypeBuilder<FitnessClassEnrolmentsEntity> builder)
        {
            builder.ToTable("FitnessClassEnrolments");

            builder
                .HasOne(p => p.Instructor)
                .WithMany(p => p.ClassEnrolments)
                .HasForeignKey(p => p.InstructorId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(p => p.FitnessClass)
                .WithMany(p => p.Enrolments)
                .HasForeignKey(p => p.FitnessClassId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}