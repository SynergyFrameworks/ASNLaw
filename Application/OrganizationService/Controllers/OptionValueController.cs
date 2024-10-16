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
    [Route("api/optionvalue")]
    [ApiController]
    [Authorize]
    public class OptionValueController : ControllerBase
    {
        private readonly IService<OptionValue, DefaultSearch<OptionValue>> _optionValueService;
        public OptionValueController(IService<OptionValue, DefaultSearch<OptionValue>> actionService)
        {
            _optionValueService = actionService;
        }


        [HttpGet("{optionvalueId}")]
        [Authorize(OptionValuesPermissions.CAN_READ)]
        public async Task<IActionResult> Get(Guid actionId)
        {
            return Ok(await _optionValueService.Get(OptionValueProjection.OptionValueInformation, actionId));
        }

        [HttpGet()]
        [Authorize(OptionValuesPermissions.CAN_READ)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _optionValueService.Get(OptionValueProjection.OptionValueInformation, HttpContext.RequestAborted));
        }

        [HttpPost("query")]
        [Authorize(OptionValuesPermissions.CAN_READ)]
        public async Task<IActionResult> Query(DefaultSearch<OptionValue> search)
        {
            return Ok(await _optionValueService.Query(search, HttpContext.RequestAborted));
        }

        [HttpPost]
        [Authorize(OptionValuesPermissions.CAN_CREATE)]
        public async Task<IActionResult> Post(OptionValueInfo newOptionValue)
        {
            if (!ModelState.IsValid) return BadRequest();

            var action = new OptionValue()
            {
              ActionOptionId = newOptionValue.ActionOptionId,
              Value = newOptionValue.Value,
              DataType = newOptionValue.DataType,
              DisplayText = newOptionValue.DisplayText,
              CreatedBy = "system",
              CreatedDate = DateTime.UtcNow
            };

            return Ok(await _optionValueService.Add(action, HttpContext.RequestAborted));
        }

        [HttpPut]
        [Authorize(OptionValuesPermissions.CAN_WRITE)]
        public async Task<IActionResult> Put(OptionValueInfo updateOptionValue)
        {
            if (!ModelState.IsValid && updateOptionValue.Id == Guid.Empty) return BadRequest();

            var action = new OptionValue()
            {
                Id = updateOptionValue.Id,
                ActionOptionId = updateOptionValue.ActionOptionId,
                Value = updateOptionValue.Value,
                DataType = updateOptionValue.DataType,
                DisplayText = updateOptionValue.DisplayText,
                ModifiedBy = "system",
                ModifiedDate = DateTime.UtcNow,
            };

            return Ok(await _optionValueService.Update(action, HttpContext.RequestAborted));
        }

        [HttpDelete]
        [Route("{optionvalueId}")]
        [Authorize(OptionValuesPermissions.CAN_DELETE)]
        public async Task<IActionResult> Delete(Guid actionId)
        {
            if (actionId == Guid.Empty) return BadRequest();
            return Ok(await _optionValueService.Delete(new OptionValue { Id = actionId, DeletedBy = "system", DeletedDate = DateTime.UtcNow }, HttpContext.RequestAborted));
        }
    }
}
