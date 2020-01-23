using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Way2.Application.Repositories.Abstractions;

namespace Way2.Presentation.WebApi.Binding
{
    /// <summary>
    /// 
    /// </summary>
    public class CityBinder : RequiredModelBinder
    {

        private readonly ICityRepository _cityRepository;

        public CityBinder(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        protected override async Task OnBindModelAsync(ModelBindingContext bindingContext)
        {

            var modelName = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);
            var value = valueProviderResult.FirstValue;

            var citiesArray = value.Split(',').Select(x => x.Trim()).ToArray();
            var cities = new List<string>();
            foreach (var cityName in citiesArray)
            {
                var city = await _cityRepository.FindAsync(x => x.Name == cityName);
                if (city == null)
                {
                    bindingContext.ModelState.TryAddModelError(modelName, $"The city '{cityName}' was not found.");
                    continue;
                }
                cities.Add(cityName);
            }

            bindingContext.Result = bindingContext.ModelState.ErrorCount == 0 ? ModelBindingResult.Success(cities) : ModelBindingResult.Failed();

        }

    }
}
