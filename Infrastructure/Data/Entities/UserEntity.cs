using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data.Entities
{
    public class UserEntity : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;   
        public string? JobTitle { get; set; }
        public UserAddressEntity? Address { get; set; }


        public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
    }
}
