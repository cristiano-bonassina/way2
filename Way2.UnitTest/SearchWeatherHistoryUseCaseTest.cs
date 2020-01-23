using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Way2.Application.Data;
using Way2.Application.Repositories.Abstractions;
using Way2.Application.UseCases;
using Xunit;

namespace Way2.UnitTest
{
    public class SearchWeatherHistoryUseCaseTest
    {

        [Fact]
        public void SearchWeatherHistoryUseCase_Should_Throw_ArgumentNullException_When_Cities_Argument_Is_Null()
        {

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                var searchWeatherHistoryUseCase = new SearchWeatherHistoryUseCase(Mock.Of<ICityWeatherRepository>());
                await searchWeatherHistoryUseCase.SearchWeatherHistoryAsync(default, DateTimeOffset.MinValue, DateTimeOffset.MinValue).ToListAsync();
            });

        }

        [Fact]
        public void SearchWeatherHistoryUseCase_Should_Throw_ArgumentNullException_When_Cities_Argument_Is_Empty()
        {

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                var cities = new List<string>();
                var searchWeatherHistoryUseCase = new SearchWeatherHistoryUseCase(Mock.Of<ICityWeatherRepository>());
                await searchWeatherHistoryUseCase.SearchWeatherHistoryAsync(cities, default, default).ToListAsync();
            });

        }

        [Fact]
        public void SearchWeatherHistoryUseCase_Should_Throw_ArgumentNullException_When_Start_Date_Is_Before_End_Date()
        {

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                var cities = new List<string> { "Florianópolis" };
                var searchWeatherHistoryUseCase = new SearchWeatherHistoryUseCase(Mock.Of<ICityWeatherRepository>());
                await searchWeatherHistoryUseCase.SearchWeatherHistoryAsync(cities, DateTimeOffset.MaxValue, DateTimeOffset.MinValue).ToListAsync();
            });

        }

        [Fact]
        public async Task Test_SearchWeatherHistoryUseCase_SearchWeatherHistoryAsync()
        {

            var cityWeatherRepositoryMock = new Mock<ICityWeatherRepository>();
            var cityNames = new[] { "Curitiba", "Florianópolis", "Porto Alegre" };
            cityWeatherRepositoryMock
                .Setup(x => x.SearchWeatherHistoryAsync(cityNames, default, default))
                .Returns(cityNames.Select(x => new CityWeatherData(x, DateTimeOffset.Now, default)).ToAsyncEnumerable);

            var searchWeatherHistoryUseCase = new SearchWeatherHistoryUseCase(cityWeatherRepositoryMock.Object);
            var weatherHistory = await searchWeatherHistoryUseCase.SearchWeatherHistoryAsync(cityNames, default, default).ToListAsync();

            foreach (var cityName in cityNames)
            {
                Assert.Single(weatherHistory.Where(x => x.City.Equals(cityName)));
            }

        }

    }
}
