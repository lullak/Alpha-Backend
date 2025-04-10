using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class AddProjectFormData
    {
        public IFormFile? Image { get; set; }
        [Required]
        public string ProjectName { get; set; } = null!;
        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Budget { get; set; }
        public DateTime Created { get; set; }

        [Required]
        public string ClientId { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

    }
}
