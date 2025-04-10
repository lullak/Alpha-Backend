using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

public class EditUserFormData
{
    [Required]
    public string Id { get; set; } = null!;
    public string? Image { get; set; }
    public IFormFile? NewImageFile { get; set; }

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;
    public string? JobTitle { get; set; }

    public string? PhoneNumber { get; set; }

    public string? StreetName { get; set; }

    public string? PostalCode { get; set; }

    public string? City { get; set; }
    public string Role { get; set; } = null!;

}