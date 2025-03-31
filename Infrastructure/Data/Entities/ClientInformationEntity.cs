using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.Entities
{
    public class ClientInformationEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ClientBillingCity { get; set; } = null!;
        public string? ClientBillingAddress { get; set; }
        public string? ClientBillingPostalCode { get; set; }
        public string? ClientBillingReference { get; set; }
        [ForeignKey(nameof(Client))]
        public string ClientID { get; set; } = null!;
        public virtual ClientEntity Client { get; set; } = null!;
    }
}
