using Microsoft.EntityFrameworkCore;
using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.DomainLayer.Enums;
using System;
using System.Collections.Generic;

namespace PokemonAPI.DomainLayer.Seed
{
    public static class DbSeeder
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.SeedUsers();
            builder.SeedAbilities();
            builder.SeedPokemon();
        }
        private static void SeedUsers(this ModelBuilder builder)
        {
            // seed test admin
            var admin = new Admin()
            {
                Id = Guid.Parse("0faee6ac-1772-4bbe-9990-a7d9a22dd529"),
                Username = "admin",
                PasswordHash = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918"
            };
            builder.Entity<Admin>().HasData(admin);
        }
        private static void SeedAbilities(this ModelBuilder builder)
        {
            var abilities = new List<Ability>()
            {
                new Ability()
                {
                    Id =  Guid.Parse("0faee6ac-1772-4bbe-9990-a7d9a22dd539"),
                    Name = "Leaf throw",
                    Description = "Throws a sharp leaf at opponent.",
                },
                new Ability()
                {
                    Id = Guid.Parse("0faee6ac-1772-4bbe-9990-a7d9a22dd549"),
                    Name = "Harden",
                    Description = "Hardens skin to repel enemy attacks.",
                },
            };

            builder.Entity<Ability>().HasData(abilities);
        }
        private static void SeedPokemon(this ModelBuilder builder)
        {
            // seed test admin
            var pokemon = new Pokemon()
            {
                Id = Guid.Parse("0faee6ac-1772-4bbe-9990-a7d9a22dd559"),
                Name = "Bulbasaur",
                Avatar = "https://i.pinimg.com/originals/3b/78/47/3b7847675982776e5219e12a680ecd84.png",
                Generation = 0,
                Height = 20,
                Type = PokemonType.Bug,
                Weight = 110,
            };

            builder.Entity<Pokemon>().HasData(pokemon);

            // https://stackoverflow.com/questions/50862525/seed-entity-with-owned-property
            var baseStat = new
            {
                PokemonId = Guid.Parse("0faee6ac-1772-4bbe-9990-a7d9a22dd559"),
                Attack = 30,
                Defense = 20,
                HealthPoints = 100,
                SpecialAttack = 60,
                SpecialDefense = 50,
                Speed = 90,
            };

            builder.Entity<Pokemon>().OwnsOne(x => x.BaseStats).HasData(baseStat);

            var abilities = new List<PokemonAbility>()
            {
                new PokemonAbility()
                {
                    Id = Guid.Parse("312c1c87-75ba-48ea-b7a0-6a31c19dae91"),
                    AbilityId = Guid.Parse("0faee6ac-1772-4bbe-9990-a7d9a22dd539"),
                    PokemonId = Guid.Parse("0faee6ac-1772-4bbe-9990-a7d9a22dd559")
                },
                new PokemonAbility()
                {
                    Id = Guid.Parse("49f2b00a-9a79-4cbe-8001-c82d0bedfaa1"),
                    AbilityId = Guid.Parse("0faee6ac-1772-4bbe-9990-a7d9a22dd549"),
                    PokemonId = Guid.Parse("0faee6ac-1772-4bbe-9990-a7d9a22dd559")
                }
            };

            builder.Entity<PokemonAbility>().HasData(abilities);
        }
    }
}
