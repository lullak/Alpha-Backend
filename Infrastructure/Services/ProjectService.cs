using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Services
{
    public interface IProjectService
    {
        Task<bool> CreateProjectAsync(AddProjectFormData formData, string defaultStatus = "started");
        Task<bool> DeleteProjectAsync(string id);
        Task<Project> GetProjectByIdAsync(string id);
        Task<IEnumerable<Project>> GetProjectsAsync();
        Task<bool> UpdateProjectAsync(EditProjectFormData formData);
    }
    public class ProjectService(IProjectRepository projectRepository, IStatusService statusService, IMemoryCache cache) : IProjectService
    {
        private readonly IProjectRepository _projectRepository = projectRepository;
        private readonly IStatusService _statusService = statusService;
        private readonly IMemoryCache _cache = cache;
        private const string _cacheKey_All = "Project_all";


        public async Task<bool> CreateProjectAsync(AddProjectFormData formData, string defaultStatus = "started")
        {
            if (formData == null)
                return false;

            var projectEntity = ProjectFactory.ToEntity(formData);
            var status = await _statusService.GetStatusByStatusNameAsync(defaultStatus);

            if (status != null && status.Id != 0)
                projectEntity.StatusId = status.Id;
            else
                return false;

            var result = await _projectRepository.AddAsync(projectEntity);
            if (result)
                _cache.Remove(_cacheKey_All);

            return result;
        }


        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            var entites = await _projectRepository.GetAllAsync(
                orderByDescending: true,
                sortBy: x => x.Created,
                filterBy: null,
                i => i.User,
                i => i.Client,
                i => i.Status
            );

            var projects = entites.Select(ProjectFactory.ToModel);
            return projects;
        }

        public async Task<Project> GetProjectByIdAsync(string id)
        {
            var entity = await _projectRepository.GetAsync(

                x => x.Id == id,
                i => i.User,
                i => i.Client,
                i => i.Status
            );

            return ProjectFactory.ToModel(entity);
        }

        public async Task<bool> UpdateProjectAsync(EditProjectFormData formData)
        {
            if (formData == null)
                return false;

            if (!await _projectRepository.ExistsAsync(x => x.Id == formData.Id))
                return false;

            var projectEntity = ProjectFactory.ToEntity(formData);
            var result = await _projectRepository.UpdateAsync(projectEntity);
            return result;
        }


        public async Task<bool> DeleteProjectAsync(string id)
        {
            var result = await _projectRepository.DeleteAsync(x => x.Id == id);
            return result;
        }
    }
}
