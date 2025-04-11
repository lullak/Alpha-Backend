using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.ProjectEndpoints
{
    public class ProjectValidationErrorExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples() => new()
        {
            Message = "Validation failed: ProjectName, StartDate, and ClientId are required."
        };
    }
}
