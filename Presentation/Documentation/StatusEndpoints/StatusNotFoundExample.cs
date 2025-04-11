using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.StatusEndpoints
{
    public class StatusNotFoundExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples() => new()
        {
            Message = "The requested status was not found."
        };
    }
}
