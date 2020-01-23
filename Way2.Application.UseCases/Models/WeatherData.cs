using System;

namespace Way2.Application.UseCases.Models
{
    public class WeatherData
    {
        
        public DateTimeOffset Date { get; }
        public float Temperature { get; }

        public WeatherData(DateTimeOffset date, float temperature)
        {
            Date = date;
            Temperature = temperature;
        }

    }
}
