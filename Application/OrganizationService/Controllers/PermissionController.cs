using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Model;
using Datalayer.Domain.Group;
using Datalayer.Domain.Security;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models;
using Infrastructure.CQRS.Projections;
using System;
using System.Threading.Tasks;
using static OrganizationService.PermissionNames;

namespace OrganizationService.Controllers
{
    [Route("api/permission")]
    [ApiController]
    [Authorize]
    public class PermissionController : ControllerBase
    {
        private readonly IService<Permission, DefaultSearch<PermissionSearchResult>> _permissionService;
        public PermissionController(IService<Permission, DefaultSearch<PermissionSearchResult>> permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet("{permissionId}")]
        [Authorize(AppPermissions.CAN_READ)]
        public async Task<IActionResult> Get(Guid permissionId)
        {
            return Ok(await _permissionService.Get(PermissionProjection.PermissionInformation, permissionId));
        }

        [HttpGet()]
        [Authorize(AppPermissions.CAN_READ)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _permissionService.Get(PermissionProjection.PermissionInformation, HttpContext.RequestAborted));
        }

        [HttpPost("query")]
        [Authorize(AppPermissions.CAN_READ)]
        public async Task<IActionResult> Query(DefaultSearch<PermissionSearchResult> search)
        {
            return Ok(await _permissionService.Query(search, HttpContext.RequestAborted));
        }

        [HttpPost]
        [Authorize(AppPermissions.CAN_CREATE)]
        public async Task<IActionResult> Post(PermissionInfo newRule)
        {
            if (!ModelState.IsValid) return BadRequest();

            var permission = new Permission()
            {
                Name = newRule.Name,
                Description = newRule.Description,
                AspNetRole = newRule.AspNetRole,
                CanCreate = newRule.CanCreate,
                CanDelete = newRule.CanDelete,
                CanRead = newRule.CanRead,
                CanWrite = newRule.CanWrite,
                CreatedBy = "system",
                CreatedDate = DateTime.UtcNow
            };

            return Ok(await _permissionService.Add(permission, HttpContext.RequestAborted));
        }

        [HttpPut]
        [Authorize(AppPermissions.CAN_WRITE)]
        public async Task<IActionResult> Put(PermissionInfo updateRule)
        {
            if (!ModelState.IsValid && updateRule.Id == Guid.Empty) return BadRequest();

            var group = new Permission()
            {
                Id = updateRule.Id,
                Name = updateRule.Name,
                Description = updateRule.Description,
                AspNetRole = updateRule.AspNetRole,
                CanCreate = updateRule.CanCreate,
                CanDelete = updateRule.CanDelete,
                CanRead = updateRule.CanRead,
                CanWrite = updateRule.CanWrite,
                ModifiedBy = "system",
                ModifiedDate = DateTime.UtcNow,
            };

            return Ok(await _permissionService.Update(group, HttpContext.RequestAborted));
        }

        [HttpDelete]
        [Route("{permissionId}")]
        [Authorize(AppPermissions.CAN_DELETE)]
        public async Task<IActionResult> Delete(Guid permissionId)
        {
            if (permissionId == Guid.Empty) return BadRequest();
            return Ok(await _permissionService.Delete(new Permission { Id = permissionId, DeletedBy = "system", DeletedDate = DateTime.UtcNow }, HttpContext.RequestAborted));
        }
    }
}
