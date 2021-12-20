using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessManager.DataAccess.Entities.EntitiesConfiguration
{
    public class FitnessClassEntityConfiguration : IEntityTypeConfiguration<FitnessClassEntity>
    {
        public void Configure(EntityTypeBuilder<FitnessClassEntity> builder)
        {
            builder.ToTable("FitnessClasses");

            builder
                .HasOne(p => p.DefaultInstructor)
                .WithMany(p => p.ClassesAssignedAsDefaultInstructor)
                .HasForeignKey(p => p.DefaultInstructorId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}