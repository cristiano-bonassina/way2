using System;

namespace Way2.Presentation.WebApi.Data
{
    public class WeatherHistoryResponse
    {
        public DateTimeOffset Date { get; set; }

        public float Temperature { get; set; }
    }
}
