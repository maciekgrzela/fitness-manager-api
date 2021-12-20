using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessManager.DataAccess.Entities.EntitiesConfiguration
{
    public class SportsEquipmentEntityConfiguration : IEntityTypeConfiguration<SportsEquipmentEntity>
    {
        public void Configure(EntityTypeBuilder<SportsEquipmentEntity> builder)
        {
            builder.ToTable("SportsEquipment");
        }
    }
}