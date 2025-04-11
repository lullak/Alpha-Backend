using Infrastructure.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.ClientEndpoints
{
    public class EditClientFormDataExample : IExamplesProvider<EditClientFormData>
    {
        public EditClientFormData GetExamples() => new()
        {
            Id = "ae5f645a-9537-40c0-9016-2fffe881b1b3",
            ClientName = "Testbolag AB",
            NewImageFile = null,
            ClientEmail = "info@testbolag.com",
            ClientPhone = "0701111111",
            ClientBillingCity = "Stockholm",
            ClientBillingAddress = "Kungsgatan 3",
            ClientBillingPostalCode = "11111",
            ClientBillingReference = "Kalle",
            ClientInformationId = "guid",
            ClientId = "guid"
        };
    }
}
