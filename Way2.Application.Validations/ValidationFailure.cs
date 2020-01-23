using Way2.Application.Validations.Abstractions;

namespace Way2.Application.Validations
{

    public class ValidationFailure : IValidationFailure
    {

        public string PropertyName { get; }
        public string Error { get; }

        public ValidationFailure(string propertyName, string error)
        {
            this.PropertyName = propertyName;
            this.Error = error;
        }

        public override string ToString()
        {
            return this.Error;
        }

    }

}
