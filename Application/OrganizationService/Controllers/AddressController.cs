using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Model;
using Datalayer.Domain.Demographics;
using Datalayer.Domain;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models;
using Infrastructure.CQRS.Projections;
using Infrastructure.CQRS.Projections.Addresses;
using System;
using System.Linq;
using System.Threading.Tasks;
using static OrganizationService.PermissionNames;

namespace OrganizationService.Controllers
{
    [Route("api/address")]
    [ApiController]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IService<Address, DefaultSearch<AddressSearchResult>> _AddressService;
        public AddressController(IService<Address, DefaultSearch<AddressSearchResult>> groupService)
        {
            _AddressService = groupService;
        }


        [HttpGet("{addressId}")]
        [Authorize(AddressPermissions.CAN_READ)]
        public async Task<IActionResult> Get(Guid AddressId)
        {
            return Ok(await _AddressService.Get(AddressProjection.AddressInformation, AddressId));
        }

        [HttpGet()]
        [Authorize(AddressPermissions.CAN_READ)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _AddressService.Get(AddressProjection.AddressInformation, HttpContext.RequestAborted));
        }

        [HttpPost("query")]
        [Authorize(AddressPermissions.CAN_READ)]
        public async Task<IActionResult> Query(DefaultSearch<AddressSearchResult> search)
        {
            return Ok(await _AddressService.Query(search, HttpContext.RequestAborted));
        }

        [HttpPost]
        [Authorize(AddressPermissions.CAN_CREATE)]
        public async Task<IActionResult> Post(AddressInfo newAddress)
        {
            if (!ModelState.IsValid) return BadRequest();

            var Address = new Address()
            {
                CountryCode = newAddress.CountryCode,
                AddressLine1 = newAddress.AddressLine1,
                AddressLine2 = newAddress.AddressLine2,
                City = newAddress.City,
                StateCode = newAddress.State,
                PostalCode = newAddress.PostalCode,
                AddressType = newAddress.AddressType,
                Clients = newAddress.Clients?.Select(clientId => new ASNClient { Id = clientId }).ToList(),
                Organizations = newAddress.Organizations?.Select(organizationId => new Datalayer.Domain.Organization { Id = organizationId }).ToList(),
                CreatedBy = "system",
                CreatedDate = DateTime.UtcNow
            };

            return Ok(await _AddressService.Add(Address, HttpContext.RequestAborted));
        }

        [HttpPut]
        [Authorize(AddressPermissions.CAN_WRITE)]
        public async Task<IActionResult> Put(AddressInfo updateAddress)
        {
            if (!ModelState.IsValid && updateAddress.Id == Guid.Empty) return BadRequest();

            var Address = new Address()
            {
                Id = updateAddress.Id,
                CountryCode = updateAddress.CountryCode,
                AddressLine1 = updateAddress.AddressLine1,
                AddressLine2 = updateAddress.AddressLine2,
                City = updateAddress.City,
                StateCode = updateAddress.State,
                PostalCode = updateAddress.PostalCode,
                AddressType = updateAddress.AddressType,
                Clients = updateAddress.Clients?.Select(clientId => new ASNClient { Id = clientId }).ToList(),
                Organizations = updateAddress.Organizations?.Select(organizationId => new Datalayer.Domain.Organization { Id = organizationId }).ToList(),
                ModifiedBy = "system",
                ModifiedDate = DateTime.UtcNow,
            };

            return Ok(await _AddressService.Update(Address, HttpContext.RequestAborted));
        }

        [HttpDelete]
        [Route("{addressId}")]
        [Authorize(AddressPermissions.CAN_DELETE)]
        public async Task<IActionResult> Delete(Guid AddressId)
        {
            if (AddressId == Guid.Empty) return BadRequest();
            return Ok(await _AddressService.Delete(new Address { Id = AddressId, DeletedBy = "system", DeletedDate = DateTime.UtcNow }, HttpContext.RequestAborted));
        }
    }
}
