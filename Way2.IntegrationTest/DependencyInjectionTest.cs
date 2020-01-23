using LogicArt.Framework.Core.Bus.Abstractions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Way2.Application.Repositories.Abstractions;
using Way2.Application.Services;
using Way2.Application.Validations.Abstractions;
using Way2.Infrastructure.Persistence;
using Way2.Presentation.WebApi;
using Xunit;

namespace Way2.IntegrationTest
{
    public class DependencyInjectionTest : IClassFixture<WebApplicationFactory<Startup>>
    {

        private readonly WebApplicationFactory<Startup> _factory;

        public DependencyInjectionTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public void EventSchemaProvider_Should_Be_Unique_Per_Scope()
        {
            using var scope = _factory.Server.Services.CreateScope();
            Assert.Same(scope.ServiceProvider.GetService<IEventSchemaProvider>(), scope.ServiceProvider.GetService<IEventSchemaProvider>());
        }

        [Fact]
        public void EventSchema_Should_Be_Singleton()
        {
            using var scopeA = _factory.Server.Services.CreateScope();
            using var scopeB = _factory.Server.Services.CreateScope();
            Assert.Same(scopeA.ServiceProvider.GetService<IEventSchema>(), scopeB.ServiceProvider.GetService<IEventSchema>());
        }

        [Fact]
        public void EventBus_Should_Be_Unique_Per_Scope()
        {
            using var scope = _factory.Server.Services.CreateScope();
            Assert.Same(scope.ServiceProvider.GetService<IEventBus>(), scope.ServiceProvider.GetService<IEventBus>());
        }

        [Fact]
        public void UnitOfWork_Should_Be_Unique_Per_Scope()
        {
            using var scope = _factory.Server.Services.CreateScope();
            Assert.Same(scope.ServiceProvider.GetService<IUnitOfWork>(), scope.ServiceProvider.GetService<IUnitOfWork>());
        }

        [Fact]
        public void CityRepository_Should_Be_Unique_Per_Scope()
        {
            using var scope = _factory.Server.Services.CreateScope();
            Assert.Same(scope.ServiceProvider.GetService<ICityRepository>(), scope.ServiceProvider.GetService<ICityRepository>());
        }

        [Fact]
        public void CityWeatherRepository_Should_Be_Unique_Per_Scope()
        {
            using var scope = _factory.Server.Services.CreateScope();
            Assert.Same(scope.ServiceProvider.GetService<ICityWeatherRepository>(), scope.ServiceProvider.GetService<ICityWeatherRepository>());
        }

        [Fact]
        public void ApplicationDbContext_Should_Be_Unique_Per_Scope()
        {
            using var scope = _factory.Server.Services.CreateScope();
            Assert.Same(scope.ServiceProvider.GetService<ApplicationDbContext>(), scope.ServiceProvider.GetService<ApplicationDbContext>());
        }

        [Fact]
        public void DbContext_Should_Be_Unique_Per_Scope()
        {
            using var scope = _factory.Server.Services.CreateScope();
            Assert.Same(scope.ServiceProvider.GetService<DbContext>(), scope.ServiceProvider.GetService<DbContext>());
        }

        [Fact]
        public void DbContext_Should_Be_Instance_Of_ApplicationDbContext()
        {
            using var scope = _factory.Server.Services.CreateScope();
            Assert.Same(scope.ServiceProvider.GetService<DbContext>(), scope.ServiceProvider.GetService<ApplicationDbContext>());
        }

        [Fact]
        public void CityValidation_Should_Be_Unique_Per_Scope()
        {
            using var scope = _factory.Server.Services.CreateScope();
            Assert.Same(scope.ServiceProvider.GetService<ICityValidation>(), scope.ServiceProvider.GetService<ICityValidation>());
        }

        [Fact]
        public void CityWeatherValidation_Should_Be_Unique_Per_Scope()
        {
            using var scope = _factory.Server.Services.CreateScope();
            Assert.Same(scope.ServiceProvider.GetService<ICityWeatherValidation>(), scope.ServiceProvider.GetService<ICityWeatherValidation>());
        }

    }
}
