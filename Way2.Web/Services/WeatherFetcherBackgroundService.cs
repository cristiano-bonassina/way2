using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Way2.Application.UseCases.Abstractions;

namespace Way2.Presentation.WebApi.Services
{
    /// <summary>
    /// 
    /// </summary>
    internal class WeatherFetcherBackgroundService : BackgroundService
    {

        private readonly IRecurringWeatherDataUpdateUseCase _recurringWeatherDataUpdateUseCase;

        public WeatherFetcherBackgroundService(IRecurringWeatherDataUpdateUseCase recurringWeatherDataUpdateUseCase)
        {
            _recurringWeatherDataUpdateUseCase = recurringWeatherDataUpdateUseCase;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _recurringWeatherDataUpdateUseCase.StartAsync(stoppingToken);
        }

    }
}
