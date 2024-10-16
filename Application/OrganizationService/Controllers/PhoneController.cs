using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Model;
using Datalayer.Domain.Demographics;
using Datalayer.Domain;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models;
using Infrastructure.CQRS.Projections;
using System;
using System.Linq;
using System.Threading.Tasks;
using static OrganizationService.PermissionNames;

namespace OrganizationService.Controllers
{
    [Route("api/phone")]
    [ApiController]
    [Authorize]
    public class PhoneController : ControllerBase
    {
        private readonly IService<Phone, DefaultSearch<PhoneSearchResult>> _phoneService;
        public PhoneController(IService<Phone, DefaultSearch<PhoneSearchResult>> phoneService)
        {
            _phoneService = phoneService;
        }


        [HttpGet("{phoneId}")]
        [Authorize(PhonePermissions.CAN_READ)]
        public async Task<IActionResult> Get(Guid phoneId)
        {
            return Ok(await _phoneService.Get(PhoneProjection.PhoneInformation, phoneId));
        }

        [HttpGet()]
        [Authorize(PhonePermissions.CAN_READ)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _phoneService.Get(PhoneProjection.PhoneInformation, HttpContext.RequestAborted));
        }

        [HttpPost("query")]
        [Authorize(PhonePermissions.CAN_READ)]
        public async Task<IActionResult> Query(DefaultSearch<PhoneSearchResult> search)
        {
            return Ok(await _phoneService.Query(search, HttpContext.RequestAborted));
        }

        [HttpPost]
        [Authorize(PhonePermissions.CAN_CREATE)]
        public async Task<IActionResult> Post(PhoneInfo newPhone)
        {
            if (!ModelState.IsValid) return BadRequest();

            var phone = new Phone()
            {
                CountryPrefix = newPhone.CountryPrefix,
                PhoneNumber = newPhone.PhoneNumber,
                PhoneType = newPhone.PhoneType,
                Clients = newPhone.Clients?.Select(clientId => new ASNClient { Id = clientId }).ToList(),
                Organizations = newPhone.Organizations?.Select(organizationId => new Datalayer.Domain.Organization { Id = organizationId }).ToList(),
                CreatedBy = "system",
                CreatedDate = DateTime.UtcNow
            };

            return Ok(await _phoneService.Add(phone, HttpContext.RequestAborted));
        }

        [HttpPut]
        [Authorize(PhonePermissions.CAN_WRITE)]
        public async Task<IActionResult> Put(PhoneInfo updatePhone)
        {
            if (!ModelState.IsValid && updatePhone.Id == Guid.Empty) return BadRequest();

            var phone = new Phone()
            {
                Id = updatePhone.Id,
                PhoneType = updatePhone.PhoneType,
                PhoneNumber = updatePhone.PhoneNumber,
                CountryPrefix = updatePhone.CountryPrefix,
                Clients = updatePhone.Clients?.Select(clientId => new ASNClient { Id = clientId }).ToList(),
                Organizations = updatePhone.Organizations?.Select(organizationId => new Datalayer.Domain.Organization { Id = organizationId }).ToList(),
                ModifiedBy = "system",
                ModifiedDate = DateTime.UtcNow,
            };

            return Ok(await _phoneService.Update(phone, HttpContext.RequestAborted));
        }

        [HttpDelete]
        [Route("{phoneId}")]
        [Authorize(PhonePermissions.CAN_DELETE)]
        public async Task<IActionResult> Delete(Guid phoneId)
        {
            if (phoneId == Guid.Empty) return BadRequest();
            return Ok(await _phoneService.Delete(new Phone { Id = phoneId, DeletedBy = "system", DeletedDate = DateTime.UtcNow }, HttpContext.RequestAborted));
        }
    }
}
