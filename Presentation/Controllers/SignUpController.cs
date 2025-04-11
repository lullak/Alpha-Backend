using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Documentation.SignInEndpoints;
using Presentation.Documentation;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using Presentation.Documentation.SignUpEndpoints;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class SignUpController : ControllerBase
    {
        private readonly IAuthService _authService;

        public SignUpController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Sign up a new user", Description = "Registers a new user with the provided data.")]
        [SwaggerRequestExample(typeof(SignUpForm), typeof(SignUpFormExample))]
        [SwaggerResponse(200, "User successfully registered")]
        [SwaggerResponse(400, "Validation failed", typeof(ErrorMessage))]
        [SwaggerResponseExample(400, typeof(SignUpValidationErrorExample))]


        public async Task<IActionResult> SignUp(SignUpForm formData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.SignUpAsync(formData);
            if (result.Succeeded)
                return Ok();

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return BadRequest(ModelState);
        }
    }
}
