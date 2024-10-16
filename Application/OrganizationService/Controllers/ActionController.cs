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
    [Route("api/action")]
    [ApiController]
    [Authorize]
    public class ActionController : ControllerBase
    {
        private readonly IService<ASNAction, DefaultSearch<ASNAction>> _actionService;
        public ActionController(IService<ASNAction, DefaultSearch<ASNAction>> actionService)
        {
            _actionService = actionService;
        }


        [HttpGet("{actionId}")]
        [Authorize(ActionPermissions.CAN_READ)]
        public async Task<IActionResult> Get(Guid actionId)
        {
            return Ok(await _actionService.Get(ASNActionProjection.ASNActionInformation, actionId));
        }

        [HttpGet()]
        [Authorize(ActionPermissions.CAN_READ)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _actionService.Get(ASNActionProjection.ASNActionInformation, HttpContext.RequestAborted));
        }

        [HttpPost("query")]
        [Authorize(ActionPermissions.CAN_READ)]
        public async Task<IActionResult> Query(DefaultSearch<ASNAction> search)
        {
            return Ok(await _actionService.Query(search, HttpContext.RequestAborted));
        }

        [HttpPost]
        [Authorize(ActionPermissions.CAN_CREATE)]
        public async Task<IActionResult> Post(ActionInfo newAction)
        {
            if (!ModelState.IsValid) return BadRequest();

            var action = new ASNAction()
            {

                Name = newAction.Name,
                Description = newAction.Description,
                ActionOptions = newAction.ActionOptions,
                CreatedBy = "system",
                CreatedDate = DateTime.UtcNow
            };

            return Ok(await _actionService.Add(action, HttpContext.RequestAborted));
        }

        [HttpPut]
        [Authorize(ActionPermissions.CAN_WRITE)]
        public async Task<IActionResult> Put(ActionInfo updateAction)
        {
            if (!ModelState.IsValid && updateAction.Id == Guid.Empty) return BadRequest();

            var action = new ASNAction()
            {
                Id = updateAction.Id,
                Name = updateAction.Name,
                Description = updateAction.Description,
                ActionOptions = updateAction.ActionOptions,
                ModifiedBy = "system",
                ModifiedDate = DateTime.UtcNow,
            };

            return Ok(await _actionService.Update(action, HttpContext.RequestAborted));
        }

        [HttpDelete]
        [Route("{actionId}")]
        [Authorize(ActionPermissions.CAN_DELETE)]
        public async Task<IActionResult> Delete(Guid actionId)
        {
            if (actionId == Guid.Empty) return BadRequest();
            return Ok(await _actionService.Delete(new ASNAction { Id = actionId, DeletedBy = "system", DeletedDate = DateTime.UtcNow }, HttpContext.RequestAborted));
        }
    }
}
