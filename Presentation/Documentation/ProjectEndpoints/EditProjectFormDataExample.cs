using Infrastructure.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.ProjectEndpoints
{
    public class EditProjectFormDataExample : IExamplesProvider<EditProjectFormData>
    {
        public EditProjectFormData GetExamples() => new()
        {
            Id = "ae5f645a-9537-40c0-9016-2fffe881b1b3",
            ProjectName = "Updated Project",
            NewImageFile = null,
            Description = "This is an updated project.",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(6),
            Budget = 15000.00m,
            Created = DateTime.UtcNow,
            ClientId = "ae5f645a-9537-40c0-9016-2fffe881b1b3",
            UserId = "ae5f645a-9537-40c0-9016-2fffe881b1b3",
            StatusId = 1
        };
    }
}
