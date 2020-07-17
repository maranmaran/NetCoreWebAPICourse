using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonAPI.DomainLayer.Entities;

namespace PokemonAPI.DomainLayer.Configurations
{
    public class BaseStatConfiguration : IEntityTypeConfiguration<BaseStat>
    {
        public void Configure(EntityTypeBuilder<BaseStat> builder)
        {
            builder
                .HasOne(x => x.Pokemon)
                .WithOne(x => x.BaseStats)
                .HasForeignKey<Pokemon>(x => x.BaseStatsId);
        }

    }
}
