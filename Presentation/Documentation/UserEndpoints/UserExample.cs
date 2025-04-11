using Infrastructure.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.UserEndpoints
{
    public class UserExample : IExamplesProvider<User>
    {
        public User GetExamples() => new()
        {
            Id = "ae5f645a-9537-40c0-9016-2fffe881b1b3",
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@domain.com",
            Image = "user-image.svg",
            JobTitle = "Software Engineer",
            PhoneNumber = "0701111111",
            StreetName = "123 Main Street",
            PostalCode = "12345",
            City = "New York",
            Role = "Admin"
        };
    }
}
