using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Way2.Presentation.WebApi.Binding
{
    /// <summary>
    /// 
    /// </summary>
    public class DateTimeBinder : RequiredModelBinder
    {
     
        protected override Task OnBindModelAsync(ModelBindingContext bindingContext)
        {

            var modelName = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            var value = valueProviderResult.FirstValue;

            try
            {

                if (long.TryParse(value, out var unixTimeMilliseconds))
                {
                    var model = DateTimeOffset.FromUnixTimeMilliseconds(unixTimeMilliseconds);
                    bindingContext.Result = ModelBindingResult.Success(model);
                    return Task.CompletedTask;
                }

                if (DateTimeOffset.TryParse(value, out var dateTime))
                {
                    bindingContext.Result = ModelBindingResult.Success(dateTime);
                    return Task.CompletedTask;
                }

                // The '+' character in the offset is being interpreted as a space, as per URL encoding rules.
                if (DateTimeOffset.TryParse(value.Replace(" ", "+"), out var anotherDateTime))
                {
                    bindingContext.Result = ModelBindingResult.Success(anotherDateTime);
                    return Task.CompletedTask;
                }

                bindingContext.ModelState.TryAddModelError(modelName, $"The parameter '{modelName}' is invalid.");
                return Task.CompletedTask;

            }
            catch (Exception)
            {
                bindingContext.ModelState.TryAddModelError(modelName, $"The parameter '{modelName}' is invalid.");
                return Task.CompletedTask;
            }

        }

    }
}
