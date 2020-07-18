using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonAPI.DomainLayer.Entities;

namespace PokemonAPI.DomainLayer.Configurations
{
    public class AbilityConfiguration : IEntityTypeConfiguration<Ability>
    {
        public void Configure(EntityTypeBuilder<Ability> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50);
        }
    }
}
