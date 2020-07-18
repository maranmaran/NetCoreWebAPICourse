using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonAPI.DomainLayer.Entities;

namespace PokemonAPI.DomainLayer.Configurations
{
    public class PokemonAbilityConfiguration: IEntityTypeConfiguration<PokemonAbility>
    {
        public void Configure(EntityTypeBuilder<PokemonAbility> builder)
        {
            builder.HasOne(x => x.Pokemon).WithMany(x => x.Abilities);
            builder.HasOne(x => x.Ability).WithMany(x => x.Pokemons);
        }
    }
}