using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PokemonAPI.BusinessLayer.Implementations.DomainServices;
using PokemonAPI.BusinessLayer.Implementations.UtilityServices;
using PokemonAPI.BusinessLayer.Interfaces;

namespace PokemonAPI.BusinessLayer
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterBusinessServices(this IServiceCollection services)
        {
            services.AddTransient<IPokemonService, PokemonService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IJwtGenerator, JwtGenerator>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
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
