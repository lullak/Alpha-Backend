using Infrastructure.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Documentation.ClientEndpoints
{
    public class ClientExample : IExamplesProvider<Client>
    {
        public Client GetExamples() => new()
        {
            Id = "ae5f645a-9537-40c0-9016-2fffe881b1b3",
            ClientName = "Testbolag AB",
            ClientImage = "acme-logo.svg",
            ClientEmail = "info@testbolag.com",
            ClientPhone = "0701111111",
            Information = new ClientInformation
            {
                Id = "ae5f645a-9537-40c0-9016-2fffe881b1b3",
                ClientBillingCity = "Stockholm",
                ClientBillingAddress = "Kungsgatan 3",
                ClientBillingPostalCode = "11111",
                ClientBillingReference = "Kalle"
            },
            Projects = new List<Project>
        {
            new Project
            {
                Id = "ae5f645a-9537-40c0-9016-2fffe881b1b3",
                ProjectName = "Project 1"
            },
            new Project
            {
                Id = "ae5f645a-9537-40c0-9016-2fffe881b1b3",
                ProjectName = "Project 2"
            }
        }
        };
    }
}
