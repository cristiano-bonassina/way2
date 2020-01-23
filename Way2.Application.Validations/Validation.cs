using System.Threading.Tasks;
using FluentValidation;
using Way2.Application.Validations.Abstractions;

namespace Way2.Application.Validations
{
    /// <summary>
    /// Base class for entity validation
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Validation<TEntity> : AbstractValidator<TEntity>, IValidation<TEntity> 
    {

        public async Task<IValidationResult> ValidateAsync(TEntity entity)
        {
            var result = new ValidationResult();
            var validationResult = await base.ValidateAsync(entity);
            foreach (var failure in validationResult.Errors)
            {
                result.AddError(new ValidationFailure(failure.PropertyName, failure.ErrorMessage));
            }
            return result;
        }

        public Task<IValidationResult> ValidateAsync(object entity)
        {
            return this.ValidateAsync((TEntity)entity);
        }

    }
}
