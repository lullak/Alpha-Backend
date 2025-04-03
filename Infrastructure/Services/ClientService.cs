using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetClientsAsync();
        Task<Client> GetUserByClientNameAsync(string clientName);
        Task<Client> GetClientByIdAsync(string id);
        Task<bool> UpdateClientAsync(EditClientFormData formData);
        Task<bool> DeleteClientAsync(string id);
        Task<bool> CreateClientAsync(AddClientFormData formData);
    }

    public class ClientService(IClientRepository clientRepository) : IClientService
    {
        private readonly IClientRepository _clientRepository = clientRepository;

        public async Task<IEnumerable<Client>> GetClientsAsync()
        {
            var entites = await _clientRepository.GetAllAsync(
                orderByDescending: false,
                sortBy: x => x.ClientName,
                filterBy: null,
                i => i.Information,
                i => i.Projects
                );
            

            var clients = entites.Select(ClientFactory.ToModel);
            return clients;

        }

        public async Task<Client> GetClientByIdAsync(string id)
        {
            var entity = await _clientRepository.GetAsync(x => x.Id == id);
            return entity == null ? null! : new Client
            {
                Id = entity.Id,
                ClientName = entity.ClientName,
            };

        }

        public async Task<Client> GetUserByClientNameAsync(string clientName)
        {
            var entity = await _clientRepository.GetAsync(x => x.ClientName.Equals(clientName));
            return entity == null ? null! : new Client
            {
                Id = entity.Id,
                ClientName = entity.ClientName,
            };
        }

        public async Task<bool> UpdateClientAsync(EditClientFormData formData)
        {
            if (formData == null)
                return false;

            if (!await _clientRepository.ExistsAsync(x => x.Id == formData.Id))
                return false;

            var clientEntity = ClientFactory.ToEntity(formData);
            var result = await _clientRepository.UpdateAsync(clientEntity);
            return result;
        }

        public async Task<bool> DeleteClientAsync(string id)
        {
            var clientEntity = await _clientRepository.GetAsync(
                x => x.Id == id,
                i => i.Projects
             );
            if (clientEntity == null || (clientEntity.Projects != null && clientEntity.Projects.Any()))
            {    
                return false;
            }

            var result = await _clientRepository.DeleteAsync(x => x.Id == id);
            return result;
        }

        public async Task<bool> CreateClientAsync(AddClientFormData formData)
        {
            if (formData == null)
                return false;

            var clientEntity = ClientFactory.ToEntity(formData);

            var result = await _clientRepository.AddAsync(clientEntity);
            return result;
        }

    }
}

