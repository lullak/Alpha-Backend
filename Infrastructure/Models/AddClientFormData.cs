using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class AddClientFormData
    {
        [Required]
        public string ClientName { get; set; } = null!;
        public string? ClientImage { get; set; }
        [Required]
        public string ClientEmail { get; set; } = null!;
        [Required]
        public string ClientPhone { get; set; } = null!;
        [Required]
        public string ClientBillingCity { get; set; } = null!;
        public string? ClientBillingAddress { get; set; }
        public string? ClientBillingPostalCode { get; set; }
        public string? ClientBillingReference { get; set; }
     
    }
}
