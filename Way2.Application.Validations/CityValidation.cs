using FluentValidation;
using Way2.Application.Validations.Abstractions;
using Way2.Domain.Entities;

namespace Way2.Application.Validations
{
    public class CityValidation : Validation<City>, ICityValidation
    {
        public CityValidation()
        {
            this.RuleFor(x => x.Name).NotEmpty();
        }
    }
}
