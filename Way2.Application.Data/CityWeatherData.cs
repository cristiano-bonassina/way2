using System;

namespace Way2.Application.Data
{
    public class CityWeatherData
    {

        public CityWeatherData(string city, DateTimeOffset date, float temperature)
        {
            this.City = city;
            this.Date = date;
            this.Temperature = temperature;
        }

        public string City { get; set; }
        public DateTimeOffset Date { get; set; }
        public float Temperature { get; set; }

    }
}
