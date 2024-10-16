using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Model;
using Datalayer.Domain.Storage;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models;
using Infrastructure.CQRS.Projections;
using System;
using System.Threading.Tasks;
using static OrganizationService.PermissionNames;

namespace OrganizationService.Controllers
{
    [Route("api/storageprovider")]
    [ApiController]
    [Authorize]
    public class StorageProviderController : ControllerBase
    {
        private readonly IService<StorageProvider, DefaultSearch<StorageProviderSearchResult>> _storageProviderService;
        public StorageProviderController(IService<StorageProvider, DefaultSearch<StorageProviderSearchResult>> storageProviderService)
        {
            _storageProviderService = storageProviderService;
        }

        [HttpGet("{storageProviderId}")]
        [Authorize(StorageProviderPermissions.CAN_READ)]
        public async Task<IActionResult> Get(Guid storageProviderId)
        {
            return Ok(await _storageProviderService.Get(StorageProviderProjection.StorageProviderInformation, storageProviderId));
        }

        [HttpGet()]
        [Authorize(StorageProviderPermissions.CAN_READ)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _storageProviderService.Get(StorageProviderProjection.StorageProviderInformation, HttpContext.RequestAborted));
        }

        [HttpPost("query")]
        [Authorize(StorageProviderPermissions.CAN_READ)]
        public async Task<IActionResult> Query(DefaultSearch<StorageProviderSearchResult> search)
        {
            return Ok(await _storageProviderService.Query(search, HttpContext.RequestAborted));
        }

        [HttpPost]
        [Authorize(StorageProviderPermissions.CAN_CREATE)]
        public async Task<IActionResult> Post(StorageProviderInfo newStorageProvider)
        {
            if (!ModelState.IsValid) return BadRequest();

            var group = new StorageProvider()
            {
                DisplayName = newStorageProvider.DisplayName,
                ProviderName = newStorageProvider.ProviderName,
                ProviderUser = newStorageProvider.ProviderUser,
                ProviderPassword = newStorageProvider.ProviderPassword,
                ClientId = newStorageProvider.ClientId,
                GroupId = newStorageProvider.GroupId,
                ClientConnection = newStorageProvider.ClientConnection,
                CreatedBy = "system",
                CreatedDate = DateTime.UtcNow
            };

            return Ok(await _storageProviderService.Add(group, HttpContext.RequestAborted));
        }

        [HttpPut]
        [Authorize(StorageProviderPermissions.CAN_WRITE)]
        public async Task<IActionResult> Put(StorageProviderInfo updateStorageProvider)
        {
            if (!ModelState.IsValid && updateStorageProvider.Id == Guid.Empty) return BadRequest();

            var group = new StorageProvider()
            {
                Id = updateStorageProvider.Id,
                DisplayName = updateStorageProvider.DisplayName,
                ProviderName = updateStorageProvider.ProviderName,
                ProviderUser = updateStorageProvider.ProviderUser,
                ProviderPassword = updateStorageProvider.ProviderPassword,
                ClientId = updateStorageProvider.ClientId,
                GroupId = updateStorageProvider.GroupId,
                ClientConnection = updateStorageProvider.ClientConnection,
                ModifiedBy = "system",
                ModifiedDate = DateTime.UtcNow
            };

            return Ok(await _storageProviderService.Update(group, HttpContext.RequestAborted));
        }

        [HttpDelete]
        [Route("{storageProviderId}")]
        [Authorize(StorageProviderPermissions.CAN_DELETE)]
        public async Task<IActionResult> Delete(Guid storageProviderId)
        {
            if (storageProviderId == Guid.Empty) return BadRequest();
            return Ok(await _storageProviderService.Delete(new StorageProvider { Id = storageProviderId, DeletedBy = "system", DeletedDate = DateTime.UtcNow }, HttpContext.RequestAborted));
        }

    }
}
