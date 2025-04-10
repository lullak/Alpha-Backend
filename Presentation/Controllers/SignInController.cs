using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

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
