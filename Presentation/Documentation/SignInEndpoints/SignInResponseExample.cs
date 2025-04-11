using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.SignInEndpoints
{
    public class SignInResponseExample : IExamplesProvider<SignInResponse>
    {
        public SignInResponse GetExamples() => new()
        {
            Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
            User = new
            {
                id = "user-123",
                name = "John Doe",
                email = "john.doe@domain.com",
                role = new[] { "Admin" }
            }
        };
    }
}
