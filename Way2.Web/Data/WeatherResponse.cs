using System.Collections.Generic;

namespace Way2.Presentation.WebApi.Data
{
    public class WeatherResponse
    {
        public string City { get; set; }

        public IEnumerable<WeatherHistoryResponse> Weather { get; set; }
    }
}