﻿using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class SignInForm
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
