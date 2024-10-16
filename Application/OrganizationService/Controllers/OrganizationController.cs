using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Model;
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
    [Route("api/organization")]
    [ApiController]
    [Authorize]
    public class OrganizationController : ControllerBase
    {
        private readonly IService<Datalayer.Domain.Organization, DefaultSearch<OrganizationSearchResult>> _orgsService;
        public OrganizationController(IService<Datalayer.Domain.Organization, DefaultSearch<OrganizationSearchResult>> orgService)
        {
            _orgsService = orgService;
        }


        [HttpGet("{orgId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(OrganizationPermissions.CAN_READ)]
        public async Task<IActionResult> Get(Guid orgId)
        {
            return Ok(await _orgsService.Get(OrganizationProjection.OrganizationInformation, orgId));
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(OrganizationPermissions.CAN_READ)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _orgsService.Get(OrganizationProjection.OrganizationInformation, HttpContext.RequestAborted));
        }

        [HttpPost("query")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(OrganizationPermissions.CAN_READ)] //TODO: Might be different (i.e. Organization.CanQuery)
        public async Task<IActionResult> Query(DefaultSearch<OrganizationSearchResult> search)
        {
            return Ok(await _orgsService.Query(search, HttpContext.RequestAborted));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(OrganizationPermissions.CAN_CREATE)]
        public async Task<IActionResult> Post(Datalayer.Domain.Organization newOrg)
        {
            if (!ModelState.IsValid) return BadRequest();

            var organization = new Datalayer.Domain.Organization()
            {
                Name = newOrg.Name,
                SuperUserId = newOrg.SuperUserId ?? null ,
                ImageUrl = newOrg.ImageUrl,
                WebUrl = newOrg.WebUrl,
                CreatedBy = "system",
                CreatedDate = DateTime.UtcNow
            };

            return Ok(await _orgsService.Add(organization, HttpContext.RequestAborted));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(OrganizationPermissions.CAN_WRITE)]
        public async Task<IActionResult> Put(OrganizationInput updateOrg)
        {
            if (!ModelState.IsValid && updateOrg.Id == Guid.Empty) return BadRequest();

            var organization = new Datalayer.Domain.Organization();

            organization.Id = updateOrg.Id;
            organization.Name = updateOrg.Name;
            organization.SuperUserId = updateOrg.SuperUserId?? null ;
            organization.ImageUrl = updateOrg.ImageUrl;
            organization.WebUrl = updateOrg.WebUrl;
            organization.ModifiedBy = "system";
            organization.ModifiedDate = DateTime.UtcNow;

            return Ok(await _orgsService.Update(organization, HttpContext.RequestAborted));
        }

        [HttpDelete]
        [Route("{orgId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(OrganizationPermissions.CAN_DELETE)]
        public async Task<IActionResult> Delete(Guid orgId)
        {
            if (orgId == Guid.Empty) return BadRequest();

            return Ok(await _orgsService.Delete(new Datalayer.Domain.Organization { Id = orgId, DeletedBy = "system", DeletedDate = DateTime.UtcNow }, HttpContext.RequestAborted));
        }
    }
}

