using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.Entities
{
    public class UserAddressEntity
    {
        [Key, ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public UserEntity User { get; set; } = null!;
        public string? StreetName { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
    }
}
