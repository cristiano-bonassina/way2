using System;
using Lamar;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Way2.Infrastructure.DependencyResolution;
using Way2.Presentation.WebApi.Conventions;
using Way2.Presentation.WebApi.Services;

namespace Way2.Presentation.WebApi
{
    public class Startup
    {

        public void ConfigureContainer(ServiceRegistry services)
        {

            const string variable = "OPEN_WEATHER_MAP_API_KEY";
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(variable)))
            {
                Environment.SetEnvironmentVariable(variable, "fc28830c17565772df3697a3b91bcd47");
            }

            // It's a good practice to avoid registering the infrastructure layer with the upper layers.
            // This can be done using the method below.
            // https://ardalis.com/avoid-referencing-infrastructure-in-visual-studio-solutions
            //
            //const string name = "Way2.Infrastructure.DependencyResolution.dll";
            //var path = Path.Combine(AppContext.BaseDirectory, name);
            //var assembly = Assembly.LoadFrom(path);
            //services.Scan(_ =>
            //{
            //    _.Assembly(assembly);
            //    _.LookForRegistries();
            //});

            services.AddLamar<InfrastructureRegistry>();

            services.AddControllers();

            services.AddHealthChecks();

            services.AddApiVersioning();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "API v1", Version = "v1" });
                options.OperationFilter<RemoveVersionFromParameter>();
                options.DocumentFilter<ReplaceVersionWithExactValueInPath>();
            });

            services.AddHostedService<DatabaseMigrationHostedService>();
            services.AddHostedService<DatabaseSeedHostedService>();
            services.AddHostedService<WeatherFetcherBackgroundService>();

        }

        public void Configure(IApplicationBuilder app)
        {

            app.UseHealthChecks("/health");
            app.UseApiVersioning();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        }

    }
}
