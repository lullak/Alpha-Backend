using Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Contexts
{
    public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<UserEntity>(options)
    {
        public virtual DbSet<UserAddressEntity> UserAddresses { get; set; }
        public virtual DbSet<ClientEntity> Clients { get; set; }
        public virtual DbSet<StatusEntity> Statuses { get; set; }
        public virtual DbSet<ProjectEntity> Projects { get; set; }
        public virtual DbSet<ClientInformationEntity> ClientInformations { get; set; }
    }
}
