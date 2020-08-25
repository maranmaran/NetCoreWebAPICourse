using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.DomainLayer.Seed;
using System;
using System.IO;

namespace PokemonAPI.DomainLayer
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<PokemonAbility> PokemonAbilities { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Trainer> Trainers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed(); // seeds database with data..

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }


    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // IDesignTimeDbContextFactory is used usually when you execute EF Core commands like Add-Migration, Update-Database, and so on
            Console.WriteLine("Which environment you wish to operate with");
            var environment = (Console.ReadLine())?.ToLower().Trim();

            if (environment != "development" &&
                environment != "release" &&
                environment != "dev" &&
                environment != "prod")
            {
                throw new Exception("Environment can only be Development or Release");
            }

            if (environment == "dev" || environment == "development")
                environment = "Development";
            if (environment == "prod" || environment == "release")
                environment = "Release";


            // Build config
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../PokemonAPI"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            // Here we create the DbContextOptionsBuilder manually.        
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseNpgsql(connectionString);

            // Create our DbContext.
            return new ApplicationDbContext(builder.Options);
        }
    }
}
