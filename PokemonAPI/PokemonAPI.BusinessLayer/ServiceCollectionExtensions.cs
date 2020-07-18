using AutoMapper;
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

        /// <summary>
        /// Configures automapper using automapper extension for dependency injection
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureAutomapper(this IServiceCollection services)
        {
            services.AddSingleton<IMapper>(provider =>
            {
                var config = new MapperConfiguration(c =>
                {
                    c.AddProfile<Mappings>();
                });

                return config.CreateMapper();
            });
        }
    }
}
