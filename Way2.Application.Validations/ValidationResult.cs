using System.Collections.Generic;
using Way2.Application.Validations.Abstractions;

namespace Way2.Application.Validations
{

    public class ValidationResult : IValidationResult
    {

        public IList<IValidationFailure> Errors { get; } = new List<IValidationFailure>();

        public bool IsValid => this.Errors.Count == 0;

        public void AddError(IValidationFailure failure)
        {
            this.Errors.Add(failure);
        }

    }

}
