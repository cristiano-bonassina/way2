using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Way2.Application.Repositories.Abstractions;
using Way2.Application.Services;
using Way2.Domain.Entities;

namespace Way2.Presentation.WebApi.Services
{
    /// <summary>
    /// Initial database data
    /// </summary>
    internal class DatabaseSeedHostedService : IHostedService
    {

        private readonly IServiceProvider _serviceProvider;

        public DatabaseSeedHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {

            using var serviceProviderScope = _serviceProvider.CreateScope();
            var unitOfWork = serviceProviderScope.ServiceProvider.GetService<IUnitOfWork>();
            await unitOfWork.BeginTransactionAsync();

            var cityNames = new List<string>
            {
                "Curitiba",
                "Florianópolis",
                "Porto Alegre"
            };

            var cityRepository = serviceProviderScope.ServiceProvider.GetService<ICityRepository>();

            foreach (var cityName in cityNames)
            {

                var existingCity = await cityRepository.FindAsync(x => x.Name == cityName);
                if (existingCity != null)
                {
                    continue;
                }

                var newCity = new City(cityName);
                await cityRepository.AddAsync(newCity);

            }

            await unitOfWork.EndTransactionAsync();

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

    }
}
