using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Way2.Presentation.WebApi;
using Xunit;

namespace Way2.IntegrationTest
{
    public class WeatherEndpointTest : IClassFixture<WebApplicationFactory<Startup>>
    {

        private readonly WebApplicationFactory<Startup> _factory;

        public WeatherEndpointTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/v1/weather/q?cities=Curitiba&start=0&end=253402300799999")]
        [InlineData("/api/v1/weather/q?cities=Florianópolis&start=0&end=253402300799999")]
        [InlineData("/api/v1/weather/q?cities=Porto Alegre&start=0&end=253402300799999")]
        [InlineData("/api/v1/weather/q?cities=Curitiba&start=0001-01-01T00:00:00+00:00&end=9999-12-31T23:59:59+00:00")]
        [InlineData("/api/v1/weather/q?cities=Florianópolis&start=0001-01-01T00:00:00+00:00&end=9999-12-31T23:59:59+00:00")]
        [InlineData("/api/v1/weather/q?cities=Porto Alegre&start=0001-01-01T00:00:00+00:00&end=9999-12-31T23:59:59+00:00")]
        public async Task Weather_Endpoint_Return_Success(string url)
        {
            var httpClient = _factory.CreateClient();
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
        }

    }
}
