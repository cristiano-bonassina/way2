using System;
using System.Threading;
using System.Threading.Tasks;

namespace Way2.Application.UseCases.Abstractions
{
    public interface IRecurringWeatherDataUpdateUseCase
    {
        Task StartAsync(CancellationToken stoppingToken, TimeSpan? recurringInterval = null);
    }
}
