using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Contexts
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<ClientEntity> Clients { get; set; }
        public virtual DbSet<StatusEntity> Statuses { get; set; }
        public virtual DbSet<ProjectEntity> Projects { get; set; }
    }
}
