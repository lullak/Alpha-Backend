using System.ComponentModel.DataAnnotations;

public class UpdateUserFormData
{
    [Required]
    public string UserId { get; set; } = null!;

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

}