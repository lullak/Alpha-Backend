using Infrastructure.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.StatusEndpoints
{
    public class StatusExample : IExamplesProvider<Status>
    {
        public Status GetExamples() => new()
        {
            Id = 1,
            StatusName = "started"
        };
    }
}
