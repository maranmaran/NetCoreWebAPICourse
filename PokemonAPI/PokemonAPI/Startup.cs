using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokemonAPI.BusinessLayer;
using PokemonAPI.BusinessLayer.Validator;
using PokemonAPI.Middleware;
using PokemonAPI.Middleware.API.Middleware;
using PokemonAPI.PersistenceLayer;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.IO;

namespace PokemonAPI
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();


            Configuration = builder.Build();
            HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // configure layers
            services.ConfigureBusinessLayer();
            services.ConfigurePersistanceLayer(Configuration);

            // configure third party libraries
            services.ConfigureAutomapper();
            services.ConfigureSwagger();

            // configure internal services
            services.ConfigureMVC();
            services.ConfigureAuthentication(Configuration);
            services.ConfigureHealthChecks();

            // configure FluentValidation
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<TrainerValidator>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokemon API v1");
                c.RoutePrefix = "api";
                c.DocExpansion(DocExpansion.None);
                c.DocumentTitle = "Pokemon API";
            });

            app.UseMiddleware<JwtMiddleware>();
            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/hc");
                endpoints.MapControllers();
            });
        }
    }
}
