using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Model;
using Datalayer.Domain.Group;
using Datalayer.Domain;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models;
using Infrastructure.CQRS.Projections;
using Infrastructure.Common.Domain;
using System;
using System.Threading.Tasks;
using static OrganizationService.PermissionNames;

namespace OrganizationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ResourceController : ControllerBase
    {
        private readonly IService<Resource, DefaultSearch<ResourceSearchResult>> _ResourceService;
        private readonly IMapper _mapper;
        public ResourceController(IService<Resource, DefaultSearch<ResourceSearchResult>> service, IMapper mapper)
        {
            _ResourceService = service;
            _mapper = mapper;
        }


        [HttpGet("{ResourceId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ResourcePermissions.CAN_READ)]

        public async Task<IActionResult> Get(Guid ResourceId)
        {
            return Ok(await _ResourceService.Get(ResourceProjection.ResourceInformation, ResourceId));
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ResourcePermissions.CAN_READ)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _ResourceService.Get(ResourceProjection.ResourceInformation, HttpContext.RequestAborted));
        }

        [HttpPost("query")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ResourcePermissions.CAN_READ)]
        public async Task<IActionResult> Query(DefaultSearch<ResourceSearchResult> search)
        {
            return Ok(await _ResourceService.Query(search, HttpContext.RequestAborted));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ResourcePermissions.CAN_CREATE)]
        public async Task<IActionResult> Post(ResourceInfo newResource)
        {
            if (!ModelState.IsValid) return BadRequest();

            Resource client = new Resource()
            {
                ResourceType = newResource.ResourceType,
                JsonValues = newResource.JsonValues,
                ModuleId = newResource.ModuleId,
                CreatedBy = "system", // Add User Information when claims are added
                CreatedDate = DateTime.UtcNow
            };

            return Ok(await _ResourceService.Add(client));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ResourcePermissions.CAN_WRITE)]
        public async Task<IActionResult> Put(ResourceInfo updateResource)
        {
            if (!ModelState.IsValid && updateResource.Id == Guid.Empty) return BadRequest();

            Resource Resource = new Resource();

            Resource.ResourceType = updateResource.ResourceType;
            Resource.JsonValues = updateResource.JsonValues;
            Resource.ModuleId = updateResource.ModuleId;
            Resource.Id = updateResource.Id;

            Resource.ModifiedBy = "system";
            Resource.ModifiedDate = DateTime.UtcNow;

            return Ok(await _ResourceService.Update(Resource));
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ResourcePermissions.CAN_DELETE)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();
            return Ok(await _ResourceService.Delete(new Resource { Id = id, DeletedBy = "system", DeletedDate = DateTime.UtcNow }));
        }
    }
}
