using System;
using System.Collections.Generic;
using Way2.Application.Data;
using Way2.Domain.Entities;

namespace Way2.Application.Repositories.Abstractions
{

    public interface ICityWeatherRepository : IRepository<CityWeather, Guid>
    {

        IAsyncEnumerable<CityWeatherData> SearchWeatherHistoryAsync(IEnumerable<string> cities, DateTimeOffset start, DateTimeOffset end);

    }

}
