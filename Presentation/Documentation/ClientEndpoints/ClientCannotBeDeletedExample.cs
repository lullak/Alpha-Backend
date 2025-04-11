using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.ClientEndpoints
{
    public class ClientCannotBeDeletedExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples() => new()
        {
            Message = "Client is active, can't be deleted."
        };
    }
}
