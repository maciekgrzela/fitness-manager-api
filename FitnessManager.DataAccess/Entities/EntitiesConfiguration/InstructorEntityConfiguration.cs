using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessManager.DataAccess.Entities.EntitiesConfiguration
{
    public class InstructorEntityConfiguration : IEntityTypeConfiguration<InstructorEntity>
    {
        public void Configure(EntityTypeBuilder<InstructorEntity> builder)
        {
            builder.ToTable("Instructors");
        }
    }
}