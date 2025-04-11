using Infrastructure.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.ProjectEndpoints
{
    public class ProjectExample : IExamplesProvider<Project>
    {
        public Project GetExamples() => new()
        {
            Id = "ae5f645a-9537-40c0-9016-2fffe881b1b3",
            ProjectName = "Sample Project",
            Image = "project-image.png",
            Description = "This is a sample project.",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(6),
            Budget = 20000.00m,
            Created = DateTime.UtcNow,
            Client = new Client
            {
                Id = "ae5f645a-9537-40c0-9016-2fffe881b1b3",
                ClientName = "Acme Corporation",
                ClientEmail = "contact@acme.com",
                ClientPhone = "0701111111"
            },
            User = new User
            {
                Id = "ae5f645a-9537-40c0-9016-2fffe881b1b3",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@domain.com"
            },
            Status = new Status
            {
                Id = 1,
                StatusName = "started"
            }
        };
    }
}
