using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Way2.Presentation.WebApi.Binding;

namespace Way2.Presentation.WebApi.Data
{
    public class WeatherRequest
    {

        [ModelBinder(BinderType = typeof(CityBinder), Name = "cities")]
        public IList<string> Cities { get; set; }

        [ModelBinder(BinderType = typeof(DateTimeBinder), Name = "end")]
        public DateTimeOffset End { get; set; }

        [ModelBinder(BinderType = typeof(DateTimeBinder), Name = "start")]
        public DateTimeOffset Start { get; set; }

    }
}
