using Infrastructure.Handlers;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Documentation;
using Presentation.Documentation.UserEndpoints;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UsersController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(Summary = "Create a new user", Description = "Creates a new user with the provided data.")]
        [SwaggerRequestExample(typeof(AddUserFormData), typeof(AddUserFormDataExample))]
        [SwaggerResponse(200, "User successfully created")]
        [SwaggerResponse(400, "Validation failed", typeof(ErrorMessage))]
        [SwaggerResponseExample(400, typeof(UserValidationErrorExample))]


        public async Task<IActionResult> Create(AddUserFormData formData)
        {
            if (!ModelState.IsValid)
                return BadRequest(formData);

            var (result, success) = await _userService.CreateUserAsync(formData);

            return success ? Ok() : BadRequest();
        }
        [HttpGet]
        [SwaggerOperation(Summary = "Get all users", Description = "Retrieves a list of all users.")]
        [SwaggerResponse(200, "Returns all users", typeof(IEnumerable<User>))]

        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetUsersAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a user by ID", Description = "Retrieves a user by their unique ID.")]
        [SwaggerResponse(200, "Returns the user", typeof(User))]
        [SwaggerResponse(404, "User not found", typeof(ErrorMessage))]
        [SwaggerResponseExample(404, typeof(UserNotFoundExample))]


        public async Task<IActionResult> Get(string id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }


        [HttpPut]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(Summary = "Update a user", Description = "Updates an existing user with the provided data.")]
        [SwaggerRequestExample(typeof(EditUserFormData), typeof(EditUserFormDataExample))]
        [SwaggerResponse(200, "User successfully updated")]
        [SwaggerResponse(404, "User not found", typeof(ErrorMessage))]
        [SwaggerResponseExample(404, typeof(UserNotFoundExample))]


        public async Task<IActionResult> Update(EditUserFormData formData)
        {
            if (!ModelState.IsValid)
                return BadRequest(formData);

            var result = await _userService.UpdateUserAsync(formData);
            return result ? Ok() : NotFound();
        }


        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a user", Description = "Deletes a user by their unique ID.")]
        [SwaggerResponse(200, "User successfully deleted")]
        [SwaggerResponse(404, "User not found", typeof(ErrorMessage))]
        [SwaggerResponseExample(404, typeof(UserNotFoundExample))]


        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            return result ? Ok() : NotFound();
        }
    }
}
