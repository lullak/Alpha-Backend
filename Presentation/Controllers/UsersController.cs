using Infrastructure.Handlers;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService, IFileHandler fileHandler) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IFileHandler _fileHandler = fileHandler;

        [HttpPost]
        public async Task<IActionResult> Create(AddUserFormData formData)
        {
            if (!ModelState.IsValid)
                return BadRequest(formData);

            //lägg till bildhantering
            //var imageFileUri = await _fileHandler.UploadFileAsync(formData.Image);

            var (result, success) = await _userService.CreateUserAsync(formData);

            return success ? Ok() : BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetUsersAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }


        [HttpPut]
        public async Task<IActionResult> Update(EditUserFormData formData)
        {
            if (!ModelState.IsValid)
                return BadRequest(formData);

            var result = await _userService.UpdateUserAsync(formData);
            return result ? Ok() : NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            return result ? Ok() : NotFound();
        }
    }
}
