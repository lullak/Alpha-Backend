using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.SignInEndpoints
{
    public class SignInValidationErrorExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples() => new()
        {
            Message = "Validation failed: Email and Password are required."
        };
    }
}
