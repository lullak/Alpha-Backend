using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.UserEndpoints
{
    public class UserAlreadyExistsExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples() => new()
        {
            Message = "A user with this email already exists."
        };
    }
}
