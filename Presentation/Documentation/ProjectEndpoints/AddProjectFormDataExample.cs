using Infrastructure.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.ProjectEndpoints
{
    public class AddProjectFormDataExample : IExamplesProvider<AddProjectFormData>
    {
        public AddProjectFormData GetExamples() => new()
        {
            ProjectName = "New Project",
            Image = null,
            Description = "This is a new project.",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(6),
            Budget = 10000.00m,
            Created = DateTime.UtcNow,
            ClientId = "ae5f645a-9537-40c0-9016-2fffe881b1b3",
            UserId = "ae5f645a-9537-40c0-9016-2fffe881b1b3"
        };
    }
}
