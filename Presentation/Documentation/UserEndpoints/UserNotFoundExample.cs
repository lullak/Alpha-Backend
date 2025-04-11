using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.UserEndpoints
{
    public class UserNotFoundExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples() => new()
        {
            Message = "User not found."
        };
    }
}
