using FluentValidation;
using Way2.Application.Validations.Abstractions;
using Way2.Domain.Entities;

namespace Way2.Application.Validations
{
    public class CityWeatherValidation : Validation<CityWeather>, ICityWeatherValidation
    {
        public CityWeatherValidation()
        {
            this.RuleFor(x => x.CityId).NotEmpty();
            this.RuleFor(x => x.Date).NotEmpty();
            this.RuleFor(x => x.Temperature).NotEmpty();
        }
    }
}
