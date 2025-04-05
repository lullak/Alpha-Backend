using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController(IStatusService statusService) : ControllerBase
    {
        private readonly IStatusService _statusService = statusService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _statusService.GetStatusesAsync();
            return Ok(result);
        }

        [HttpGet("{statusName}")]
        public async Task<IActionResult> Get(string statusName)
        {
            var result = await _statusService.GetStatusByStatusNameAsync(statusName);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
