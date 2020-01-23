using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Way2.Application.UseCases.Abstractions;
using Way2.Presentation.WebApi.Data;

namespace Way2.Presentation.WebApi.Controllers.V1
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WeatherController : Controller
    {

        private readonly ISearchWeatherHistoryUseCase _searchWeatherHistoryUseCase;

        public WeatherController(ISearchWeatherHistoryUseCase searchWeatherHistoryUseCase)
        {
            _searchWeatherHistoryUseCase = searchWeatherHistoryUseCase;
        }

        [HttpGet("q")]
        [Produces("application/json", Type = typeof(WeatherResponse))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(WeatherResponse), StatusCodes.Status200OK)]
        public async IAsyncEnumerable<WeatherResponse> SearchWeatherHistoryAsync([FromQuery]WeatherRequest request)
        {

            var weatherHistory = _searchWeatherHistoryUseCase.SearchWeatherHistoryAsync(request.Cities, request.Start, request.End);
            var weatherHistoryGroupedByCity = weatherHistory.GroupBy(x => x.City);

            await foreach (var cityWeather in weatherHistoryGroupedByCity)
            {
                yield return new WeatherResponse
                {
                    City = cityWeather.Key,
                    Weather = cityWeather.Select(x => new WeatherHistoryResponse { Date = x.Date, Temperature = x.Temperature }).ToEnumerable()
                };
            }

        }

    }
}
