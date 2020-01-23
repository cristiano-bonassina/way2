using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Way2.Application.Repositories.Abstractions;
using Way2.Application.Services;
using Way2.Application.Validations;
using Way2.Application.Validations.Exceptions;
using Way2.Domain.Entities;
using Way2.Presentation.WebApi;
using Xunit;

namespace Way2.IntegrationTest
{
    public class EntityValidationTest : IClassFixture<WebApplicationFactory<Startup>>
    {

        private readonly WebApplicationFactory<Startup> _factory;

        public EntityValidationTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Should_Throw_ValidationException_When_City_Name_Is_Null()
        {

            await Assert.ThrowsAsync<ValidationException>(async () =>
            {

                using var scope = _factory.Server.Services.CreateScope();
                var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
                await unitOfWork.BeginTransactionAsync();

                var cityService = scope.ServiceProvider.GetService<ICityRepository>();
                var city = new City(default);
                await cityService.AddAsync(city);

                await unitOfWork.EndTransactionAsync();

            });

        }

        [Fact]
        public async Task Should_Throw_ValidationException_When_City_Name_Is_Empty()
        {

            await Assert.ThrowsAsync<ValidationException>(async () =>
            {

                using var scope = _factory.Server.Services.CreateScope();
                var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
                await unitOfWork.BeginTransactionAsync();

                var cityService = scope.ServiceProvider.GetService<ICityRepository>();
                var city = new City("");
                await cityService.AddAsync(city);

                await unitOfWork.EndTransactionAsync();

            });

        }

        [Fact]
        public async Task Should_Throw_ValidationException_When_CityWeather_CityId_Is_Empty()
        {

            await Assert.ThrowsAsync<ValidationException>(async () =>
            {

                using var scope = _factory.Server.Services.CreateScope();
                var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
                await unitOfWork.BeginTransactionAsync();

                var cityWeatherService = scope.ServiceProvider.GetService<ICityWeatherRepository>();
                var city = new CityWeather(default, default, default);
                await cityWeatherService.AddAsync(city);

                await unitOfWork.EndTransactionAsync();

            });

        }

        [Fact]
        public async Task Should_Throw_ValidationException_When_CityWeather_Date_Is_Empty()
        {

            await Assert.ThrowsAsync<ValidationException>(async () =>
            {

                using var scope = _factory.Server.Services.CreateScope();
                var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
                await unitOfWork.BeginTransactionAsync();

                var cityWeatherService = scope.ServiceProvider.GetService<ICityWeatherRepository>();
                var city = new CityWeather(default, default, default);
                await cityWeatherService.AddAsync(city);

                await unitOfWork.EndTransactionAsync();

            });

        }

    }
}
