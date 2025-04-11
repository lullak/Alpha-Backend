using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.UserEndpoints
{
    public class EditUserFormDataExample : IExamplesProvider<EditUserFormData>
    {
        public EditUserFormData GetExamples() => new()
        {
            Id = "ae5f645a-9537-40c0-9016-2fffe881b1b3",
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@domain.com",
            NewImageFile = null, 
            JobTitle = "Senior Software Engineer",
            PhoneNumber = "0701111111",
            StreetName = "123 Main Street",
            PostalCode = "12345",
            City = "New York",
            Role = "Admin"
        };
    }
}
