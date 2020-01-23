using System;
using System.Collections.Generic;
using Way2.Application.Data;

namespace Way2.Application.UseCases.Abstractions
{
    public interface ISearchWeatherHistoryUseCase
    {
        IAsyncEnumerable<CityWeatherData> SearchWeatherHistoryAsync(IList<string> cities, DateTimeOffset start, DateTimeOffset end);
    }
}
