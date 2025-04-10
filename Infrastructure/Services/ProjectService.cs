using Infrastructure.Factories;
using Infrastructure.Handlers;
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
    public class ProjectService(IProjectRepository projectRepository, IStatusService statusService, IMemoryCache cache, IFileHandler fileHandler) : IProjectService
    {
        private readonly IProjectRepository _projectRepository = projectRepository;
        private readonly IStatusService _statusService = statusService;
        private readonly IMemoryCache _cache = cache;
        private const string _cacheKey_All = "Project_all";
        private readonly IFileHandler _fileHandler = fileHandler;


        public async Task<bool> CreateProjectAsync(AddProjectFormData formData, string defaultStatus = "started")
        {
            if (formData == null)
                return false;

            string? imageFileUri = null;
            if (formData.Image != null)
            {
                imageFileUri = await _fileHandler.UploadFileAsync(formData.Image);
            }

            var projectEntity = ProjectFactory.ToEntity(formData, imageFileUri);
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
            if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<Project>? cachedItems))
                return cachedItems;

 
            var projects = await SetCache();

            return projects;
        }
        public async Task<IEnumerable<Project>> SetCache()
        {
            _cache.Remove(_cacheKey_All);
            var entites = await _projectRepository.GetAllAsync(
               orderByDescending: true,
               sortBy: x => x.Created,
               filterBy: null,
               i => i.User,
               i => i.Client,
               i => i.Status
           );

            var projects = entites.Select(ProjectFactory.ToModel);
            _cache.Set(_cacheKey_All, projects, TimeSpan.FromMinutes(10));

            return projects;
        }

        public async Task<Project?> GetProjectByIdAsync(string id)
        {

            if (_cache.TryGetValue(_cacheKey_All, out IEnumerable<Project>? cachedItems))
            {
                var cachedProject = cachedItems?.FirstOrDefault(x => x.Id == id);
                if (cachedProject != null)
                    return cachedProject;
            }

            var entity = await _projectRepository.GetAsync(

                x => x.Id == id,
                i => i.User,
                i => i.Client,
                i => i.Status
            );

            if (entity == null)
                return null;

            var project = ProjectFactory.ToModel(entity);

            await SetCache();

            return project;
        }

        public async Task<bool> UpdateProjectAsync(EditProjectFormData formData)
        {
            if (formData == null)
                return false;

            if (!await _projectRepository.ExistsAsync(x => x.Id == formData.Id))
                return false;

            string? imageFileUri = formData?.Image; 

            if (formData.NewImageFile != null && formData.NewImageFile.Length > 0)
            {
                imageFileUri = await _fileHandler.UploadFileAsync(formData.NewImageFile);
            }

            var projectEntity = ProjectFactory.ToEntity(formData, imageFileUri);
            var result = await _projectRepository.UpdateAsync(projectEntity);
            if (result)
            {
                _cache.Remove(_cacheKey_All);
            }
            return result;
        }


        public async Task<bool> DeleteProjectAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            var project = await _projectRepository.GetAsync(x => x.Id == id);
            if (project == null)
                return false;


            var result = await _projectRepository.DeleteAsync(x => x.Id == id);
            if (result)
            {
                _cache.Remove(_cacheKey_All);
            }
            return result;
        }
    }
}
