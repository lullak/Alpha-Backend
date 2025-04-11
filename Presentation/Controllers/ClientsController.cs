using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Documentation;
using Presentation.Documentation.ClientEndpoints;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ClientsController(IClientService clientService) : ControllerBase
    {
        private readonly IClientService _clientService = clientService;


        [HttpGet]
        [SwaggerOperation(Summary = "Get all clients", Description = "Retrieves a list of all clients.")]
        [SwaggerResponse(200, "Returns all clients", typeof(IEnumerable<Client>))]

        public async Task<IActionResult> GetAll()
        {
            var result = await _clientService.GetClientsAsync();
            return Ok(result);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(Summary = "Create a new client", Description = "Creates a new client with the provided data.")]
        [SwaggerRequestExample(typeof(AddClientFormData), typeof(AddClientFormDataExample))]
        [SwaggerResponse(200, "Client successfully created")]
        [SwaggerResponse(400, "Validation failed", typeof(ErrorMessage))]
        [SwaggerResponseExample(400, typeof(ClientValidationErrorExample))]


        public async Task<IActionResult> Create(AddClientFormData formData)
        {
            if (!ModelState.IsValid)
                return BadRequest(formData);

            var result = await _clientService.CreateClientAsync(formData);
            return result ? Ok() : BadRequest();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a client by ID", Description = "Retrieves a client by their unique ID.")]
        [SwaggerResponse(200, "Returns the client", typeof(Client))]
        [SwaggerResponse(404, "Client not found", typeof(ErrorMessage))]
        [SwaggerResponseExample(404, typeof(ClientNotFoundExample))]

        public async Task<IActionResult> Get(string id)
        {
            var result = await _clientService.GetClientByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }


        [HttpPut]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(Summary = "Update a client", Description = "Updates an existing client with the provided data.")]
        [SwaggerRequestExample(typeof(EditClientFormData), typeof(EditClientFormDataExample))]
        [SwaggerResponse(200, "Client successfully updated")]
        [SwaggerResponse(404, "Client not found", typeof(ErrorMessage))]
        [SwaggerResponseExample(404, typeof(ClientNotFoundExample))]

        public async Task<IActionResult> Update(EditClientFormData formData)
        {
            if (!ModelState.IsValid)
                return BadRequest(formData);

            var result = await _clientService.UpdateClientAsync(formData);
            return result ? Ok() : NotFound();
        }


        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a client", Description = "Deletes a client by their unique ID.")]
        [SwaggerResponse(200, "Client successfully deleted")]
        [SwaggerResponse(405, "Client is active, can't be deleted", typeof(ErrorMessage))]
        [SwaggerResponseExample(405, typeof(ClientCannotBeDeletedExample))] 

        public async Task<IActionResult> Delete(string id)
        {
            var result = await _clientService.DeleteClientAsync(id);
            return result ? Ok() : StatusCode(StatusCodes.Status405MethodNotAllowed,"Client is active, can't be deleted");
        }
    }
}
