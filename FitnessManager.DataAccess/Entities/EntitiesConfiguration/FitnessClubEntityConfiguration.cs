using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessManager.DataAccess.Entities.EntitiesConfiguration
{
    public class FitnessClubEntityConfiguration : IEntityTypeConfiguration<FitnessClubEntity>
    {
        public void Configure(EntityTypeBuilder<FitnessClubEntity> builder)
        {
            builder.ToTable("FitnessClubs");

            builder
                .HasOne(p => p.FitnessClubNetwork)
                .WithMany(p => p.FitnessClubs)
                .HasForeignKey(p => p.FitnessClubNetworkId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasMany(p => p.Halls)
                .WithOne(p => p.FitnessClub)
                .HasForeignKey(p => p.FitnessClubId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}