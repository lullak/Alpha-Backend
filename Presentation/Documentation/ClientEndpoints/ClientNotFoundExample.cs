using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.ClientEndpoints
{
    public class ClientNotFoundExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples() => new()
        {
            Message = "Client not found."
        };
    }
}
