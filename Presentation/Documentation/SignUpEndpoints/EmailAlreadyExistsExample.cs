using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.SignUpEndpoints
{
    public class EmailAlreadyExistsExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples() => new()
        {
            Message = "A user with this email already exists."
        };
    }
}
