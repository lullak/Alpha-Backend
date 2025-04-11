using Infrastructure.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.SignInEndpoints
{
    public class SignInFormExample : IExamplesProvider<SignInForm>
    {
        public SignInForm GetExamples() => new()
        {
            Email = "john.doe@domain.com",
            Password = "P@ssw0rd!"
        };
    }
}
