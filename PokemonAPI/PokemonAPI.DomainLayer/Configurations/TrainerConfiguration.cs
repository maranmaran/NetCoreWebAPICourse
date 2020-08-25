using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonAPI.DomainLayer.Entities;

namespace PokemonAPI.DomainLayer.Configurations
{
    public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.OwnsOne(x => x.Address);
            builder.Property(x => x.FirstName).HasMaxLength(50);
            builder.Property(x => x.LastName).HasMaxLength(50);

            builder.HasMany(t => t.CaughtPokemons)
                    .WithOne(p => p.Trainer)
                    .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
