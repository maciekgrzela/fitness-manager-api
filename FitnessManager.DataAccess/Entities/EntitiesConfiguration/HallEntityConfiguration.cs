using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessManager.DataAccess.Entities.EntitiesConfiguration
{
    public class HallEntityConfiguration : IEntityTypeConfiguration<HallEntity>
    {
        public void Configure(EntityTypeBuilder<HallEntity> builder)
        {
            builder.ToTable("Halls");

            builder
                .HasMany(p => p.SportsEquipments)
                .WithOne(p => p.Hall)
                .HasForeignKey(p => p.HallId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}