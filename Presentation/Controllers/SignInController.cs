using Infrastructure.Manager;
using Infrastructure.Models;
using Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, ITokenManager TokenManager) : ControllerBase
    {
        private readonly SignInManager<UserEntity> _signInManager = signInManager;
        private readonly UserManager<UserEntity> _userManager = userManager;
        private readonly ITokenManager _TokenManager = TokenManager;

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInForm model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var signInResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (signInResult.Succeeded)
                    {
                        var token = _TokenManager.GenerateJwtToken(user!, "User");
                        return Ok(new
                        {
                            token,
                            user = new
                            {
                                id = user.Id,
                                name = user.UserName,
                                email = user.Email,
                                role = await _userManager.GetRolesAsync(user)
                            }
                        });
                    }
                }
            }

            return Unauthorized(new { error = "Invalid email or password." });
        }
    }
}
