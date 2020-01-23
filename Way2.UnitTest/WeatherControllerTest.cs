using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Way2.Application.Data;
using Way2.Application.UseCases.Abstractions;
using Way2.Presentation.WebApi.Controllers.V1;
using Way2.Presentation.WebApi.Data;
using Xunit;

namespace Way2.UnitTest
{
    public class WeatherControllerTest
    {

        [Fact]
        public async Task Test_WeatherController_SearchWeatherHistoryAsync()
        {

            var cityWeatherDataList = new List<CityWeatherData> { new CityWeatherData("Florianópolis", DateTimeOffset.MaxValue, int.MaxValue) };
            var searchWeatherHistoryUseCase = new Mock<ISearchWeatherHistoryUseCase>();
            searchWeatherHistoryUseCase
                .Setup(x => x.SearchWeatherHistoryAsync(default, default, default))
                .Returns(cityWeatherDataList.ToAsyncEnumerable());

            using var weatherController = new WeatherController(searchWeatherHistoryUseCase.Object);
            var response = await weatherController.SearchWeatherHistoryAsync(new WeatherRequest()).ToListAsync();

            var cityWeatherData = cityWeatherDataList.First();
            var weatherResponse = response.First();

            Assert.Equal(cityWeatherData.City, weatherResponse.City);
            Assert.Equal(cityWeatherData.Date, weatherResponse.Weather.First().Date);
            Assert.Equal(cityWeatherData.Temperature, weatherResponse.Weather.First().Temperature);

        }

    }
}
