using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessManager.DataAccess.Entities.EntitiesConfiguration
{
    public class DepartmentEntityConfiguration : IEntityTypeConfiguration<DepartmentEntity>
    {
        public void Configure(EntityTypeBuilder<DepartmentEntity> builder)
        {
            builder.ToTable("Departments");

            builder
                .HasOne(p => p.FitnessClub)
                .WithMany(p => p.Departments)
                .HasForeignKey(p => p.FitnessClubId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}