using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.ProjectEndpoints
{
    public class ProjectNotFoundExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples() => new()
        {
            Message = "Project not found."
        };
    }
}
