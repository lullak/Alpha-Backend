using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.SignInEndpoints
{
    public class InvalidEmailOrPasswordExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples() => new()
        {
            Message = "Invalid email or password."
        };
    }
}
