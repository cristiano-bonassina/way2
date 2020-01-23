using System.Threading.Tasks;
using OpenWeatherMap.Standard;
using Way2.Application.UseCases.Models;
using Way2.Application.UseCases.Services;

namespace Way2.Infrastructure.DependencyResolution.Services
{
    /// <summary>
    /// Provides weather information
    /// </summary>
    public class WeatherProvider : IWeatherProvider
    {

        private readonly Current _current;

        public WeatherProvider(Current current)
        {
            _current = current;
        }

        public async Task<WeatherData> GetWeatherDataByCityNameAsync(string city)
        {

            var weather = await _current.GetWeatherDataByCityName(city);
            if (weather == null)
            {
                return null;
            }

            return new WeatherData(weather.AcquisitionDateTime, weather.WeatherDayInfo.Temperature);

        }
    }
}
