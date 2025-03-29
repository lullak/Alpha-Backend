using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetUsersAsync();
            return Ok(result);
        }
    }
}
