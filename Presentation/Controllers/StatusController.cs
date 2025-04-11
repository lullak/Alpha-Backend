using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Documentation;
using Presentation.Documentation.SignUpEndpoints;
using Presentation.Documentation.StatusEndpoints;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class StatusController(IStatusService statusService) : ControllerBase
    {
        private readonly IStatusService _statusService = statusService;

        [HttpGet]
        [SwaggerOperation(Summary = "Get all statuses", Description = "Retrieves a list of all available statuses.")]
        [SwaggerResponse(200, "Returns all statuses", typeof(IEnumerable<Status>))]

        public async Task<IActionResult> GetAll()
        {
            var result = await _statusService.GetStatusesAsync();
            return Ok(result);
        }

        [HttpGet("{statusName}")]
        [SwaggerOperation(Summary = "Get a status by name", Description = "Retrieves a status by its unique name.")]
        [SwaggerResponse(200, "Returns the status", typeof(Status))]
        [SwaggerResponse(404, "Status not found", typeof(ErrorMessage))]
        [SwaggerResponseExample(404, typeof(StatusNotFoundExample))]

        public async Task<IActionResult> Get(string statusName)
        {
            var result = await _statusService.GetStatusByStatusNameAsync(statusName);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
