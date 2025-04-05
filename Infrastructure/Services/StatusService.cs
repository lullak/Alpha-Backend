using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Services
{
    public interface IStatusService
    {
        Task<Status> GetStatusByStatusNameAsync(string statusName);
        Task<IEnumerable<Status>> GetStatusesAsync();
    }
    public class StatusService(IStatusRepository statusRepository, IMemoryCache cache) : IStatusService
    {
        private readonly IStatusRepository _statusRepository = statusRepository;
        private readonly IMemoryCache _cache = cache;
        private const string _cacheKey_All = "Status_all";

        public async Task<IEnumerable<Status>> GetStatusesAsync()
        {
            if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<Status>? cachedItems))
                return cachedItems;

            var entites = await _statusRepository.GetAllAsync(sortBy: x => x.Id);

            _cache.Remove(_cacheKey_All);
            var statuses = entites.Select(entity => new Status
            {
                Id = entity.Id,
                StatusName = entity.StatusName
            });

            _cache.Set(_cacheKey_All, statuses, TimeSpan.FromMinutes(10));

            return statuses;
        }

        public async Task<Status> GetStatusByStatusNameAsync(string statusName)
        {
            if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<Status>? cachedItems))
            {
                var cachedProject = cachedItems?.FirstOrDefault(x => statusName.Equals(statusName));
                if (cachedProject != null)
                    return cachedProject;
            }
            var entity = await _statusRepository.GetAsync(x => x.StatusName == statusName);
            if (entity == null)
                return null!;

            _cache.Remove(_cacheKey_All);
            var status = new Status
            {
                Id = entity.Id,
                StatusName = entity.StatusName
            };

            _cache.Set(_cacheKey_All, status, TimeSpan.FromMinutes(10));

            return status;
        }
    }
}

