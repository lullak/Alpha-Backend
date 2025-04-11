using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.ClientEndpoints
{
    public class ClientValidationErrorExample : IExamplesProvider<ErrorMessage>
    {
        public ErrorMessage GetExamples() => new()
        {
            Message = "Validation failed: ClientName, ClientEmail, and ClientPhone are required."
        };
    }
}
