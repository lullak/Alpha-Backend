using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.SignUpEndpoints
{
    public class SignUpValidationErrorExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples() => new()
        {
            Message = "Validation failed: FirstName, LastName, Email, and Password are required."
        };
    }
}
