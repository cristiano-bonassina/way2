using System.Collections.Generic;

namespace Way2.Application.Validations.Abstractions
{
    public interface IValidationResult
    {

        void AddError(IValidationFailure failure);

        IList<IValidationFailure> Errors { get; }    
        
        bool IsValid { get; }

    }
}
