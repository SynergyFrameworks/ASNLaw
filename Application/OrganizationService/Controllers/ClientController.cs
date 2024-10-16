using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Model;
using Datalayer.Domain;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models;
using Infrastructure.CQRS.Projections;
using System;
using System.Threading.Tasks;
using static OrganizationService.PermissionNames;


namespace OrganizationService.Controllers
{
    [Route("api/client")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IService<ASNClient, DefaultSearch<ClientSearchResult>> _clientService;
        public ClientController(IService<ASNClient, DefaultSearch<ClientSearchResult>> clientService)
        {
            _clientService = clientService;
        }


        [HttpGet("{clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ClientPermissions.CAN_READ)]
        public async Task<IActionResult> Get(Guid clientId)
        {
            return Ok(await _clientService.Get(ClientProjection.ClientInformation, clientId));
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ClientPermissions.CAN_READ)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _clientService.Get(ClientProjection.ClientInformation, HttpContext.RequestAborted));
        }

        [HttpPost("query")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ClientPermissions.CAN_READ)]
        public async Task<IActionResult> Query(DefaultSearch<ClientSearchResult> search)
        {
            return Ok(await _clientService.Query(search, HttpContext.RequestAborted));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ClientPermissions.CAN_CREATE)]
        public async Task<IActionResult> Post(ClientInfo newClient)
        {
            if (!ModelState.IsValid) return BadRequest();

            var client = new ASNClient()
            {
                ClientNo = newClient.ClientNo,
                Name = newClient.Name,
                Description = newClient.Description,
                ImageUrl = newClient.ImageUrl,
                WebUrl = newClient.WebUrl,
                OrganizationId = newClient.OrganizationId,
                CreatedBy = "system",
                CreatedDate = DateTime.UtcNow
            };

            return Ok(await _clientService.Add(client, HttpContext.RequestAborted));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ClientPermissions.CAN_WRITE)]
        public async Task<IActionResult> Put(ClientInfo updateClient)
        {
            if (!ModelState.IsValid && updateClient.Id == Guid.Empty) return BadRequest();

            ASNClient modifiedClient = new ASNClient
            {
                Id = updateClient.Id,
                ClientNo = updateClient.ClientNo,
                Name = updateClient.Name,
                Description = updateClient.Description,
                ImageUrl = updateClient.ImageUrl,
                WebUrl = updateClient.WebUrl,
                OrganizationId = updateClient.OrganizationId,
                ModifiedBy = "system",
                ModifiedDate = DateTime.UtcNow,
            };

            return Ok(await _clientService.Update(modifiedClient, HttpContext.RequestAborted));
        }

        [HttpDelete]
        [Route("{clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ClientPermissions.CAN_DELETE)]
        public async Task<IActionResult> Delete(Guid clientId)
        {
            if (clientId == Guid.Empty) return BadRequest();
            return Ok(await _clientService.Delete(new ASNClient { Id = clientId, DeletedBy = "system", DeletedDate = DateTime.UtcNow }, HttpContext.RequestAborted));
        }
    }
}
