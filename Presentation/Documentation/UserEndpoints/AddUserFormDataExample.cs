using Infrastructure.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.UserEndpoints
{
    public class AddUserFormDataExample : IExamplesProvider<AddUserFormData>
    {
        public AddUserFormData GetExamples() => new()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@domain.com",
            Image = null, 
            JobTitle = "Software Engineer",
            PhoneNumber = "0701111111",
            StreetName = "123 Main Street",
            PostalCode = "12345",
            City = "New York",
            Role = "Admin"
        };
    }
}
