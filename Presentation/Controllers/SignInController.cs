using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Documentation;
using Presentation.Documentation.SignInEndpoints;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class SignInController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost]
        [SwaggerOperation(Summary = "Sign in a user", Description = "Authenticates a user and returns a JWT token if successful.")]
        [SwaggerRequestExample(typeof(SignInForm), typeof(SignInFormExample))]
        [SwaggerResponse(200, "User successfully signed in", typeof(SignInResponse))]
        [SwaggerResponse(400, "Validation failed", typeof(ErrorMessage))]
        [SwaggerResponseExample(400, typeof(SignInValidationErrorExample))]
        [SwaggerResponse(401, "Invalid email or password", typeof(ErrorMessage))]
        [SwaggerResponseExample(401, typeof(InvalidEmailOrPasswordExample))]


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
