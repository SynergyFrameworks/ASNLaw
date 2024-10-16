using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Model;
using Datalayer.Domain.Group;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Projections;
using System;
using System.Threading.Tasks;
using static OrganizationService.PermissionNames;

namespace OrganizationService.Controllers
{
    [Route("api/actionoption")]
    [ApiController]
    [Authorize]
    public class ActionOptionController : ControllerBase
    {
        private readonly IService<ActionOption, DefaultSearch<ActionOption>> _actionOptionService;
        public ActionOptionController(IService<ActionOption, DefaultSearch<ActionOption>> actionService)
        {
            _actionOptionService = actionService;
        }


        [HttpGet("{actionoptionId}")]
        [Authorize(ActionOptionPermissions.CAN_READ)]
        public async Task<IActionResult> Get(Guid actionId)
        {
            return Ok(await _actionOptionService.Get(ActionOptionProjection.ActionOptionInformation, actionId));
        }

        [HttpGet()]
        [Authorize(ActionOptionPermissions.CAN_READ)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _actionOptionService.Get(ActionOptionProjection.ActionOptionInformation, HttpContext.RequestAborted));
        }

        [HttpPost("query")]
        [Authorize(ActionOptionPermissions.CAN_READ)]
        public async Task<IActionResult> Query(DefaultSearch<ActionOption> search)
        {
            return Ok(await _actionOptionService.Query(search, HttpContext.RequestAborted));
        }

        [HttpPost]
        [Authorize(ActionOptionPermissions.CAN_CREATE)]
        public async Task<IActionResult> Post(ActionOptionInfo newAction)
        {
            if (!ModelState.IsValid) return BadRequest();

            var action = new ActionOption()
            {

              Label  = newAction.Label,
              ActionId = newAction.ActionId,
              CreatedBy = "system",
              CreatedDate = DateTime.UtcNow
            };

            return Ok(await _actionOptionService.Add(action, HttpContext.RequestAborted));
        }

        [HttpPut]
        [Authorize(ActionOptionPermissions.CAN_WRITE)]
        public async Task<IActionResult> Put(ActionOptionInfo updateAction)
        {
            if (!ModelState.IsValid && updateAction.Id == Guid.Empty) return BadRequest();

            var action = new ActionOption()
            {
                Id = updateAction.Id,
                Label = updateAction.Label,
                ActionId = updateAction.ActionId,
                ModifiedBy = "system",
                ModifiedDate = DateTime.UtcNow,
            };

            return Ok(await _actionOptionService.Update(action, HttpContext.RequestAborted));
        }

        [HttpDelete]
        [Route("{actionoptionId}")]
        [Authorize(ActionOptionPermissions.CAN_DELETE)]
        public async Task<IActionResult> Delete(Guid actionId)
        {
            if (actionId == Guid.Empty) return BadRequest();
            return Ok(await _actionOptionService.Delete(new ActionOption { Id = actionId, DeletedBy = "system", DeletedDate = DateTime.UtcNow }, HttpContext.RequestAborted));
        }
    }
}
