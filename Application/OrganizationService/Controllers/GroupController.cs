using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Model;
using Datalayer.Domain.Group;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models;
using Infrastructure.CQRS.Projections;
using System;
using System.Threading.Tasks;
using static OrganizationService.PermissionNames;

namespace OrganizationService.Controllers
{
    [Route("api/group")]
    [ApiController]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly IService<ASNGroup, DefaultSearch<GroupSearchResult>> _groupService;
        public GroupController(IService<ASNGroup, DefaultSearch<GroupSearchResult>> groupService)
        {
            _groupService = groupService;
        }


        [HttpGet("{groupId}")]
        [Authorize(GroupPermissions.CAN_READ)]
        public async Task<IActionResult> Get(Guid groupId)
        {
            return Ok(await _groupService.Get(GroupProjection.GroupInformation, groupId));
        }

        [HttpGet()]
        [Authorize(GroupPermissions.CAN_READ)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _groupService.Get(GroupProjection.GroupInformation, HttpContext.RequestAborted));
        }

        [HttpPost("query")]
        [Authorize(GroupPermissions.CAN_READ)]
        public async Task<IActionResult> Query(DefaultSearch<GroupSearchResult> search)
        {
            return Ok(await _groupService.Query(search, HttpContext.RequestAborted));
        }

        [HttpPost]
        [Authorize(GroupPermissions.CAN_CREATE)]
        public async Task<IActionResult> Post(GroupInfo newGroup)
        {
            if (!ModelState.IsValid) return BadRequest();

            var group = new ASNGroup()
            {
                Name = newGroup.Name,
                Description = newGroup.Description,
                ImageUrl = newGroup.ImageUrl,
                TeamId = newGroup.TeamId,
                CreatedBy = "system",
                CreatedDate = DateTime.UtcNow
            };

            return Ok(await _groupService.Add(group, HttpContext.RequestAborted));
        }

        [HttpPut]
        [Authorize(GroupPermissions.CAN_WRITE)]
        public async Task<IActionResult> Put(GroupInfo updateGroup)
        {
            if (!ModelState.IsValid && updateGroup.Id == Guid.Empty) return BadRequest();

            var group = new ASNGroup()
            {
                Id = updateGroup.Id,
                Name = updateGroup.Name,
                Description = updateGroup.Description,
                ImageUrl = updateGroup.ImageUrl,
                TeamId = updateGroup.TeamId,
                ModifiedBy = "system",
                ModifiedDate = DateTime.UtcNow,
            };

            return Ok(await _groupService.Update(group, HttpContext.RequestAborted));
        }

        [HttpDelete]
        [Route("{groupId}")]
        [Authorize(GroupPermissions.CAN_DELETE)]
        public async Task<IActionResult> Delete(Guid groupId)
        {
            if (groupId == Guid.Empty) return BadRequest();
            return Ok(await _groupService.Delete(new ASNGroup { Id = groupId, DeletedBy = "system", DeletedDate = DateTime.UtcNow }, HttpContext.RequestAborted));
        }
    }
}
