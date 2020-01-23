using System.Threading.Tasks;

namespace Way2.Application.Validations.Abstractions
{

    public interface IValidation
    {
        Task<IValidationResult> ValidateAsync(object entity);
    }

    public interface IValidation<TEntity> : IValidation
    {
        Task<IValidationResult> ValidateAsync(TEntity entity);
    }

}
