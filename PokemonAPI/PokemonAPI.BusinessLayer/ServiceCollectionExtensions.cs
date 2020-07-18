using Microsoft.Extensions.DependencyInjection;
using PokemonAPI.BusinessLayer.Implementations;
using PokemonAPI.BusinessLayer.Interfaces;

namespace PokemonAPI.BusinessLayer
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterBusinessServices(this IServiceCollection services)
        {
            services.AddTransient<IPokemonService, PokemonService>();
        }
    }
}
