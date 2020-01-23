using System;
using System.Collections.Generic;
using System.Linq;
using Way2.Application.Data;
using Way2.Application.Repositories.Abstractions;
using Way2.Application.UseCases.Abstractions;

namespace Way2.Application.UseCases
{
    /// <summary>
    /// This use case makes it possible to consult the city's weather history
    /// </summary>
    public class SearchWeatherHistoryUseCase : ISearchWeatherHistoryUseCase
    {

        private readonly ICityWeatherRepository _cityWeatherRepository;

        public SearchWeatherHistoryUseCase(ICityWeatherRepository cityWeatherRepository)
        {
            _cityWeatherRepository = cityWeatherRepository;
        }

        public IAsyncEnumerable<CityWeatherData> SearchWeatherHistoryAsync(IList<string> cities, DateTimeOffset start, DateTimeOffset end)
        {

            if (cities == null)
            {
                throw new ArgumentNullException(nameof(cities));
            }

            if (!cities.Any())
            {
                throw new ArgumentException(nameof(cities));
            }

            if (end < start)
            {
                throw new ArgumentException(nameof(cities));
            }

            return _cityWeatherRepository.SearchWeatherHistoryAsync(cities, start, end);

        }

    }
}
