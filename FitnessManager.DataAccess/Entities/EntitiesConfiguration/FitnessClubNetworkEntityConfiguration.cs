using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessManager.DataAccess.Entities.EntitiesConfiguration
{
    public class FitnessClubNetworkEntityConfiguration : IEntityTypeConfiguration<FitnessClubNetworkEntity>
    {
        public void Configure(EntityTypeBuilder<FitnessClubNetworkEntity> builder)
        {
            builder.ToTable("FitnessClubNetworks");
        }
    }
}