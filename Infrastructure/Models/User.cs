﻿using Infrastructure.Data.Entities;

namespace Infrastructure.Models
{
    public class User
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Image { get; set; }
        public string? StreetName { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string Role { get; set; } = null!;
        public string? JobTitle { get; set; }

    }
}
