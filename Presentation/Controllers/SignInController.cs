using Infrastructure.Manager;
using Infrastructure.Models;
using Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInForm model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { error = "Invalid input." });

            var response = await _authService.SignInAsync(model);

            if (response == null)
                return Unauthorized(new { error = "Invalid email or password." });

            return Ok(response);
        }
    }
}
