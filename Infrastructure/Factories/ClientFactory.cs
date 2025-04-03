using Infrastructure.Data.Entities;
using Infrastructure.Models;

namespace Infrastructure.Factories

{
    public class ClientFactory
    {
        public static Client ToModel(ClientEntity entity)
        {
            return entity == null
                ? null!
                : new Client
                {
                    Id = entity.Id,
                    ClientImage = entity.ClientImage,
                    ClientName = entity.ClientName,
                    ClientEmail = entity.ClientEmail,
                    ClientPhone = entity.ClientPhone,
                    Information = new ClientInformation
                    {
                        Id = entity.Information.Id,
                        ClientBillingCity = entity.Information.ClientBillingCity,
                        ClientBillingAddress = entity.Information.ClientBillingAddress,
                        ClientBillingPostalCode = entity.Information.ClientBillingPostalCode,
                        ClientBillingReference = entity.Information.ClientBillingReference

                    },
                    Projects = entity.Projects.Select(p => new Project
                    {
                        Id = p.Id,
                        ProjectName = p.ProjectName,

                    }).ToList()
                };
        }

        public static ClientEntity ToEntity(EditClientFormData formData)
        {
            return formData == null
                ? null!
                : new ClientEntity
                {
                    Id = formData.Id,
                    ClientImage = formData.ClientImage,
                    ClientName = formData.ClientName,
                    ClientEmail = formData.ClientEmail,
                    ClientPhone = formData.ClientPhone,
                    Information = new ClientInformationEntity
                    {
                        Id = formData.ClientInformationId,
                        ClientBillingCity = formData.ClientBillingCity,
                        ClientBillingAddress = formData.ClientBillingAddress,
                        ClientBillingPostalCode = formData.ClientBillingPostalCode,
                        ClientBillingReference = formData.ClientBillingReference,
                        ClientID = formData.ClientId
                    }
                };
        }

        public static ClientEntity ToEntity(AddClientFormData formData)
        {
            return formData == null
                ? null!
                : new ClientEntity
                {
                    ClientImage = formData.ClientImage,
                    ClientName = formData.ClientName,
                    ClientEmail = formData.ClientEmail,
                    ClientPhone = formData.ClientPhone,
                    Information = new ClientInformationEntity
                    {
                        ClientBillingCity = formData.ClientBillingCity,
                        ClientBillingAddress = formData.ClientBillingAddress,
                        ClientBillingPostalCode = formData.ClientBillingPostalCode,
                        ClientBillingReference = formData.ClientBillingReference
                    }
                };
        }
    }
}
