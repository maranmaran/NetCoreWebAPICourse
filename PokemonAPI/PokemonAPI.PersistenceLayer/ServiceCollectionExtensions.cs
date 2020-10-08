using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokemonAPI.DomainLayer;
using PokemonAPI.PersistenceLayer.Interfaces;
using PokemonAPI.PersistenceLayer.Repositories;

namespace PokemonAPI.PersistenceLayer
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigurePersistanceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // Add database
            services.AddDbContext<ApplicationDbContext>(o =>
            {
                o.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
                o.EnableSensitiveDataLogging();
                o.EnableDetailedErrors();
            });

            // Configure context for DI
            services.AddTransient<ApplicationDbContext>();

            // add repositories to DI
            services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));
            //services.AddTransient<IPokemonRepository, PokemonRepository>();
        }


    }
}
