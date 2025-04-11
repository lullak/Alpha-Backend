using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Documentation;
using Presentation.Documentation.ProjectEndpoints;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ProjectsController(IProjectService projectService) : ControllerBase
    {
        private readonly IProjectService _projectService = projectService;


        [HttpPost]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(Summary = "Create a new project", Description = "Creates a new project with the provided data.")]
        [SwaggerRequestExample(typeof(AddProjectFormData), typeof(AddProjectFormDataExample))]
        [SwaggerResponse(200, "Project successfully created")]
        [SwaggerResponse(400, "Validation failed", typeof(ErrorMessage))]
        [SwaggerResponseExample(400, typeof(ProjectValidationErrorExample))]

        public async Task<IActionResult> Create(AddProjectFormData formData)
        {
            if (!ModelState.IsValid)
                return BadRequest(formData);

            var result = await _projectService.CreateProjectAsync(formData);
            return result ? Ok() : BadRequest();
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all projects", Description = "Retrieves a list of all projects.")]
        [SwaggerResponse(200, "Returns all projects", typeof(IEnumerable<Project>))]

        public async Task<IActionResult> GetAll()
        {
            var result = await _projectService.GetProjectsAsync();
            return Ok(result);
        }


        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a project by ID", Description = "Retrieves a project by its unique ID.")]
        [SwaggerResponse(200, "Returns the project", typeof(Project))]
        [SwaggerResponse(404, "Project not found", typeof(ErrorMessage))]
        [SwaggerResponseExample(404, typeof(ProjectNotFoundExample))] 

        public async Task<IActionResult> Get(string id)
        {
            var result = await _projectService.GetProjectByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }


        [HttpPut]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(Summary = "Update a project", Description = "Updates an existing project with the provided data.")]
        [SwaggerRequestExample(typeof(EditProjectFormData), typeof(EditProjectFormDataExample))]
        [SwaggerResponse(200, "Project successfully updated")]
        [SwaggerResponse(404, "Project not found", typeof(ErrorMessage))]
        [SwaggerResponseExample(404, typeof(ProjectNotFoundExample))]


        public async Task<IActionResult> Update(EditProjectFormData formData)
        {
            if (!ModelState.IsValid)
                return BadRequest(formData);

            var result = await _projectService.UpdateProjectAsync(formData);
            return result ? Ok() : NotFound();
        }


        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a project", Description = "Deletes a project by its unique ID.")]
        [SwaggerResponse(200, "Project successfully deleted")]
        [SwaggerResponse(404, "Project not found", typeof(ErrorMessage))]
        [SwaggerResponseExample(404, typeof(ProjectNotFoundExample))]


        public async Task<IActionResult> Delete(string id)
        {
            var result = await _projectService.DeleteProjectAsync(id);
            return result ? Ok() : NotFound();
        }
    }
}
