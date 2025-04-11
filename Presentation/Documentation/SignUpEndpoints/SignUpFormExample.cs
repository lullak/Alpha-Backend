using Infrastructure.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.SignUpEndpoints
{
    public class SignUpFormExample : IExamplesProvider<SignUpForm>
    {
        public SignUpForm GetExamples() => new()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@domain.com",
            Password = "P@ssw0rd!",
            Role = "User"
        };
    }
}
