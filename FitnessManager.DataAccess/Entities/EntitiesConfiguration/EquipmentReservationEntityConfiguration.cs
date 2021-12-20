using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessManager.DataAccess.Entities.EntitiesConfiguration
{
    public class EquipmentReservationEntityConfiguration : IEntityTypeConfiguration<EquipmentReservationEntity>
    {
        public void Configure(EntityTypeBuilder<EquipmentReservationEntity> builder)
        {
            builder.ToTable("EquipmentReservations");

            builder
                .HasOne(p => p.SportsEquipment)
                .WithMany(p => p.EquipmentReservations)
                .HasForeignKey(p => p.SportsEquipmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}