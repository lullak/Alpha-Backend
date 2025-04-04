using Infrastructure.Data.Contexts;
using Infrastructure.Data.Entities;

namespace Infrastructure.Repositories
{
    public interface IProjectRepository : IBaseRepository<ProjectEntity>
    {
        Task<ProjectEntity?> GetByIdAsync(string id);
    }
    public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
    {
        public async Task<ProjectEntity?> GetByIdAsync(string id)
        {
            return await GetAsync(p => p.Id == id);
        }
    }
}
