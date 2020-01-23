using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Way2.Application.Repositories.Abstractions;
using Way2.Application.Services;
using Way2.Application.UseCases.Abstractions;
using Way2.Domain.Entities;
using IWeatherProvider = Way2.Application.UseCases.Services.IWeatherProvider;

namespace Way2.Application.UseCases
{
    /// <summary>
    /// This use case makes it possible to update the city's weather data over a period of time.
    /// </summary>
    public class RecurringWeatherDataUpdateUseCase : IRecurringWeatherDataUpdateUseCase
    {

        private readonly ICityRepository _cityRepository;
        private readonly ICityWeatherRepository _cityWeatherRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWeatherProvider _weatherProvider;

        public RecurringWeatherDataUpdateUseCase(IWeatherProvider weatherProvider, IUnitOfWork unitOfWork, ICityRepository cityRepository, ICityWeatherRepository cityWeatherRepository)
        {
            _weatherProvider = weatherProvider;
            _cityRepository = cityRepository;
            _cityWeatherRepository = cityWeatherRepository;
            _unitOfWork = unitOfWork;
        }

        public Task StartAsync(CancellationToken cancellationToken, TimeSpan? recurringInterval = null)
        {

            if (recurringInterval == null)
            {
                recurringInterval = TimeSpan.FromMinutes(15);
            }

            return Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await this.UpdateWeatherDataAsync();
                    await Task.Delay(recurringInterval.Value, cancellationToken);
                }
            }, cancellationToken);

        }

        private async Task UpdateWeatherDataAsync()
        {

            try
            {

                await _unitOfWork.BeginTransactionAsync();

                var allCities = _cityRepository.Query().ToAsyncEnumerable();
                await foreach (var city in allCities)
                {

                    var cityCurrentWeather = await _weatherProvider.GetWeatherDataByCityNameAsync(city.Name);
                    if (cityCurrentWeather == null)
                    {
                        continue;
                    }

                    var cityWeather = new CityWeather(city.Id, cityCurrentWeather.Date, cityCurrentWeather.Temperature);
                    await _cityWeatherRepository.AddAsync(cityWeather);

                }

                await _unitOfWork.EndTransactionAsync();

            }
            catch (Exception)
            {
                await _unitOfWork.EndTransactionAsync(false);
            }

        }

    }
}
