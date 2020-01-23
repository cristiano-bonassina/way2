using System;
using System.Text;
using Way2.Application.Validations.Abstractions;

namespace Way2.Application.Validations.Exceptions
{
    public class ValidationException : Exception
    {

        public IValidationResult Result { get; }

        public ValidationException(IValidationResult result)
        {
            this.Result = result;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var error in this.Result.Errors)
            {
                if (sb.Length > 0)
                {
                    sb.Append(",");
                }
                sb.AppendLine(error.ToString());
            }
            return sb.ToString();
        }

    }
}
