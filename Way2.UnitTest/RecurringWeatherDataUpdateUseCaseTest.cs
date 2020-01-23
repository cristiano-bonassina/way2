using System;
using System.Linq;
using System.Threading;
using Moq;
using Way2.Application.Repositories.Abstractions;
using Way2.Application.Services;
using Way2.Application.UseCases;
using Way2.Application.UseCases.Models;
using Way2.Application.UseCases.Services;
using Way2.Domain.Entities;
using Xunit;

namespace Way2.UnitTest
{
    public class RecurringWeatherDataUpdateUseCaseTest
    {

        [Fact]
        public void Test_RecurringWeatherDataUpdateUseCase_StartAsync()
        {

            var cityNames = new[] { "Curitiba", "Florianópolis", "Porto Alegre" };
            var weatherProviderMock = new Mock<IWeatherProvider>();
            foreach (var cityName in cityNames)
            {
                weatherProviderMock
                    .Setup(x => x.GetWeatherDataByCityNameAsync(cityName))
                    .ReturnsAsync(new WeatherData(DateTimeOffset.Now, default));
            }
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var cityWeatherRepositoryMock = new Mock<ICityWeatherRepository>();
            var cityRepositoryMock = new Mock<ICityRepository>();
            cityRepositoryMock
                .Setup(x => x.Query())
                .Returns(cityNames.Select(cityName => new City(cityName) { Id = Guid.NewGuid() }).AsQueryable());

            var searchWeatherHistoryUseCase = new RecurringWeatherDataUpdateUseCase(weatherProviderMock.Object, unitOfWorkMock.Object, cityRepositoryMock.Object, cityWeatherRepositoryMock.Object);
            searchWeatherHistoryUseCase.StartAsync(CancellationToken.None).Wait(TimeSpan.FromSeconds(5));

            unitOfWorkMock.Verify(x => x.BeginTransactionAsync(), Times.Once);
            unitOfWorkMock.Verify(x => x.EndTransactionAsync(true), Times.Once);
            cityWeatherRepositoryMock.Verify(x => x.AddAsync(It.IsAny<CityWeather>()), Times.Exactly(3));

        }

    }
}
