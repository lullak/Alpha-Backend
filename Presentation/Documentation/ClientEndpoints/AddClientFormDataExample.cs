using Infrastructure.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.ClientEndpoints
{
    public class AddClientFormDataExample : IExamplesProvider<AddClientFormData>
    {
        public AddClientFormData GetExamples() => new()
        {
            ClientName = "Testbolag AB",
            ClientImage = null, 
            ClientEmail = "info@testbolag.com",
            ClientPhone = "0701111111",
            ClientBillingCity = "Stockholm",
            ClientBillingAddress = "Kungsgatan 3",
            ClientBillingPostalCode = "11111",
            ClientBillingReference = "Kalle"
        };
    }
}
