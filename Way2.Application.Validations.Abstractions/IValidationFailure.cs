namespace Way2.Application.Validations.Abstractions
{
    public interface IValidationFailure
    {

        string Error { get; }

        string PropertyName { get; }        

    }
}
