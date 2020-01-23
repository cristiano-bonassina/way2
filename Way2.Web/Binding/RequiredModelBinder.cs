using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Way2.Presentation.WebApi.Binding
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class RequiredModelBinder : IModelBinder
    {
        public virtual Task BindModelAsync(ModelBindingContext bindingContext)
        {

            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                bindingContext.ModelState.TryAddModelError(modelName, $"A value for the '{modelName}' parameter or property was not provided.");
                return Task.CompletedTask;
            }

            var value = valueProviderResult.FirstValue;
            if (string.IsNullOrEmpty(value))
            {
                bindingContext.ModelState.TryAddModelError(modelName, $"A value for the '{modelName}' parameter or property was not provided.");
                return Task.CompletedTask;
            }

            return OnBindModelAsync(bindingContext);

        }

        protected abstract Task OnBindModelAsync(ModelBindingContext bindingContext);

    }

}
