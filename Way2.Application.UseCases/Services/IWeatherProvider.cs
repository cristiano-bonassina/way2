using System.Threading.Tasks;
using Way2.Application.UseCases.Models;

namespace Way2.Application.UseCases.Services
{
    public interface IWeatherProvider
    {
        Task<WeatherData> GetWeatherDataByCityNameAsync(string name);
    }
}
