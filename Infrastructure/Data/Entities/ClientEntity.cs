using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data.Entities
{
    public class ClientEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ClientName { get; set; } = null!;
        public string? ClientImage { get; set; }
        public string ClientEmail { get; set; } = null!;
        public string ClientPhone { get; set; } = null!;
        public DateTime Created { get; set; } = DateTime.Now;

        public virtual ClientInformationEntity Information { get; set; } = null!;
        public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
    }
}
