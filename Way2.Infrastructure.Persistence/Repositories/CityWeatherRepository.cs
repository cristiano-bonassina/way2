using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Way2.Application.Data;
using Way2.Application.Repositories.Abstractions;
using Way2.Domain.Entities;

namespace Way2.Infrastructure.Persistence.Repositories
{
    public class CityWeatherRepository : Repository<CityWeather, Guid>, ICityWeatherRepository
    {

        /// <summary>
        /// Returns the weather history
        /// Precompiled query to maximize performance
        /// https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ef/language-reference/compiled-queries-linq-to-entities
        /// </summary>
        private static readonly
            Func<DbContext, IEnumerable<string>, DateTimeOffset, DateTimeOffset, IAsyncEnumerable<CityWeatherData>>
            SearchWeatherHistoryAsyncQuery = EF.CompileAsyncQuery(
                (DbContext context, IEnumerable<string> cities, DateTimeOffset from, DateTimeOffset to) =>
                    context
                        .Set<CityWeather>()
                        .Include(x => x.City)
                        .Where(cityWeather =>
                            cities.Contains(cityWeather.City.Name) &&
                            cityWeather.Date >= from &&
                            cityWeather.Date <= to)
                        .OrderBy(x => x.City.Name)
                        .ThenBy(x => x.Date)
                        .Select(x => new CityWeatherData(x.City.Name, x.Date, x.Temperature))
                        .AsNoTracking()
            );

        public CityWeatherRepository(ApplicationDbContext context) : base(context) { }

        public IAsyncEnumerable<CityWeatherData> SearchWeatherHistoryAsync(IEnumerable<string> cities, DateTimeOffset start, DateTimeOffset end)
        {
            return SearchWeatherHistoryAsyncQuery(this.Context, cities, start, end);
        }

    }
}
