using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.DomainLayer.Enums;
using System;

namespace PokemonAPI.DomainLayer.Configurations
{
    public class PokemonConfiguration : IEntityTypeConfiguration<Pokemon>
    {
        public void Configure(EntityTypeBuilder<Pokemon> builder)
        {
            builder
                .HasOne(x => x.BaseStats)
                .WithOne(x => x.Pokemon)
                .HasForeignKey<BaseStat>(x => x.PokemonId);

            builder.Property(x => x.Type).HasConversion(
                x => x.ToString(),
                x => (PokemonType)Enum.Parse(typeof(PokemonType), x)
            );

            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.Avatar).HasMaxLength(50);
        }
    }
}
