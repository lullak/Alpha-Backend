using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Services
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetClientsAsync();
        Task<Client> GetClientByIdAsync(string id);
        Task<bool> UpdateClientAsync(EditClientFormData formData);
        Task<bool> DeleteClientAsync(string id);
        Task<bool> CreateClientAsync(AddClientFormData formData);
        Task<IEnumerable<Client>> SetCache();
    }

    public class ClientService(IClientRepository clientRepository, IMemoryCache cache) : IClientService
    {
        private readonly IClientRepository _clientRepository = clientRepository;
        private readonly IMemoryCache _cache = cache;
        private const string _cacheKey_All = "Client_all";

        public async Task<IEnumerable<Client>> GetClientsAsync()
        {
            if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<Client>? cachedItems))
                return cachedItems;

            var clients = await SetCache();
            return clients;

        }

        public async Task<Client> GetClientByIdAsync(string id)
        {

            if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<Client>? cachedItems))
            {
                var cachedClient = cachedItems?.FirstOrDefault(x => x.Id == id);
                if (cachedClient != null)
                    return cachedClient;
            }

            var entity = await _clientRepository.GetAsync(x => x.Id == id);

            if (entity == null)
                return null!;

            var client = ClientFactory.ToModel(entity);

            await SetCache();

            return client;

        }

        public async Task<bool> UpdateClientAsync(EditClientFormData formData)
        {
            if (formData == null)
                return false;

            if (!await _clientRepository.ExistsAsync(x => x.Id == formData.Id))
                return false;

            var clientEntity = ClientFactory.ToEntity(formData);
            var result = await _clientRepository.UpdateAsync(clientEntity);
            if (result)
            {
                _cache.Remove(_cacheKey_All);
            }
            return result;
        }

        public async Task<bool> DeleteClientAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;
            var clientEntity = await _clientRepository.GetAsync(
                x => x.Id == id,
                i => i.Projects
             );
            if (clientEntity == null || (clientEntity.Projects != null && clientEntity.Projects.Any()))
            {    
                return false;
            }

            var result = await _clientRepository.DeleteAsync(x => x.Id == id);
            if (result)
            {
                _cache.Remove(_cacheKey_All);
            }
            return result;
        }

        public async Task<bool> CreateClientAsync(AddClientFormData formData)
        {
            if (formData == null)
                return false;

            var clientEntity = ClientFactory.ToEntity(formData);

            var result = await _clientRepository.AddAsync(clientEntity);
            if (result)
                _cache.Remove(_cacheKey_All);
            return result;
        }

        public async Task<IEnumerable<Client>> SetCache()
        {
            _cache.Remove(_cacheKey_All);
            var entites = await _clientRepository.GetAllAsync(
               orderByDescending: false,
                sortBy: x => x.ClientName,
                filterBy: null,
                i => i.Information,
                i => i.Projects
                );

            var clients = entites.Select(ClientFactory.ToModel);
            _cache.Set(_cacheKey_All, clients, TimeSpan.FromMinutes(10));

            return clients;
        }

    }
}

