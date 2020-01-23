using System;
using Lamar;
using LogicArt.Framework.Core.Bus;
using LogicArt.Framework.Core.Bus.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenWeatherMap.Standard;
using Way2.Application.Events.Schema;
using Way2.Application.UseCases;
using Way2.Application.Validations;
using Way2.Infrastructure.DependencyResolution.Conventions;
using Way2.Infrastructure.Persistence;
using Way2.Infrastructure.Persistence.Repositories;

namespace Way2.Infrastructure.DependencyResolution
{
    /// <summary>
    /// With this class you can register the infrastructure in the upper layers
    /// </summary>
    public class InfrastructureRegistry : ServiceRegistry
    {
        public InfrastructureRegistry()
        {

            // Current assembly services registration
            this.Scan(_ =>
            {
                _.TheCallingAssembly();
                _.Convention<DefaultRegistrationConvention>();
            });

            // Use cases assembly services registration
            this.Scan(_ =>
            {
                _.Assembly(typeof(SearchWeatherHistoryUseCase).Assembly);
                _.Convention<DefaultRegistrationConvention>();
            });

            // Events assembly services registration
            this.Scan(_ =>
            {
                _.Assembly(typeof(EventBus).Assembly);
                _.Assembly(typeof(EventSchemaProvider).Assembly);
                _.Convention<DefaultRegistrationConvention>();
            });

            // Repositories assembly services registration
            this.Scan(_ =>
            {
                _.Assembly(typeof(Repository<,>).Assembly);
                _.Convention<DefaultRegistrationConvention>();
            });

            // Validations assembly services registration
            this.Scan(_ =>
            {
                _.Assembly(typeof(Validation<>).Assembly);
                _.Convention<DefaultRegistrationConvention>();
            });

            // Weather provider service single instance registration
            this.AddSingleton(x =>
            {
                var appId = Environment.GetEnvironmentVariable("OPEN_WEATHER_MAP_API_KEY");
                return new Current(appId);
            });

            // Entity events provider service single instance registration
            this.AddSingleton<IEventSchema>(context => context.GetService<IEventSchemaProvider>().GetSchema());

            // Entity framework database context registration
            this.AddScoped<DbContext>(context => context.GetService<ApplicationDbContext>());
            this.AddScoped<ApplicationDbContext>();

        }
    }
}
