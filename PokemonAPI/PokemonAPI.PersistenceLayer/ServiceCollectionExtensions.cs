using Microsoft.Extensions.DependencyInjection;
using PokemonAPI.PersistenceLayer.Interfaces;
using PokemonAPI.PersistenceLayer.Repositories;

namespace PokemonAPI.PersistenceLayer
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IPokemonRepository, PokemonRepository>();
        }
    }
}
