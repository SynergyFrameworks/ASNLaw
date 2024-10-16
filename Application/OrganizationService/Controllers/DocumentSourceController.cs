using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Model;
using Datalayer.Domain.Storage;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Projections;
using System;
using System.Threading.Tasks;
using static OrganizationService.PermissionNames;

namespace OrganizationService.Controllers
{
    [Route("api/documentSource")]
    [ApiController]
    [Authorize]
    public class DocumentSourceController : ControllerBase
    {
        private readonly IService<DocumentSource, DefaultSearch<DocumentSource>> _documentSourceService;
        public DocumentSourceController(IService<DocumentSource, DefaultSearch<DocumentSource>> documentSourceService)
        {
            _documentSourceService = documentSourceService;
        }


        [HttpGet("{documentSourceId}")]
        [Authorize(DocumentSourcePermissions.CAN_READ)]
        public async Task<IActionResult> Get(Guid documentSourceId)
        {
            return Ok(await _documentSourceService.Get(DocumentSourceProjection.DocumentSourceInformation, documentSourceId));
        }

        [HttpGet()]
        [Authorize(DocumentSourcePermissions.CAN_READ)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _documentSourceService.Get(DocumentSourceProjection.DocumentSourceInformation, HttpContext.RequestAborted));
        }

        [HttpPost("query")]
        [Authorize(DocumentSourcePermissions.CAN_READ)]
        public async Task<IActionResult> Query(DefaultSearch<DocumentSource> search)
        {
            return Ok(await _documentSourceService.Query(search, HttpContext.RequestAborted));
        }

        [HttpPost]
        [Authorize(DocumentSourcePermissions.CAN_CREATE)]
        public async Task<IActionResult> Post(DocumentSourceInfo newDocumentSource)
        {
            if (!ModelState.IsValid) return BadRequest();

            var documentSource = new DocumentSource()
            {
                Description = newDocumentSource.Description,
                InputFolder = newDocumentSource.InputFolder,
                OutputFolder = newDocumentSource.OutputFolder,
                StorageProviderId = newDocumentSource.StorageProviderId,
                CreatedBy = "system",
                CreatedDate = DateTime.UtcNow
            };

            return Ok(await _documentSourceService.Add(documentSource, HttpContext.RequestAborted));
        }

        [HttpPut]
        [Authorize(DocumentSourcePermissions.CAN_WRITE)]
        public async Task<IActionResult> Put(DocumentSourceInfo updateDocumentSource)
        {
            if (!ModelState.IsValid && updateDocumentSource.Id == Guid.Empty) return BadRequest();

            var documentSource = new DocumentSource()
            {
                Id = updateDocumentSource.Id,
                Description = updateDocumentSource.Description,
                InputFolder = updateDocumentSource.InputFolder,
                OutputFolder = updateDocumentSource.OutputFolder,
                StorageProviderId = updateDocumentSource.StorageProviderId,
                ModifiedBy = "system",
                ModifiedDate = DateTime.UtcNow,
            };

            return Ok(await _documentSourceService.Update(documentSource, HttpContext.RequestAborted));
        }

        [HttpDelete]
        [Route("{documentSourceId}")]
        [Authorize(DocumentSourcePermissions.CAN_DELETE)]
        public async Task<IActionResult> Delete(Guid documentSourceId)
        {
            if (documentSourceId == Guid.Empty) return BadRequest();
            return Ok(await _documentSourceService.Delete(new DocumentSource { Id = documentSourceId, DeletedBy = "system", DeletedDate = DateTime.UtcNow }, HttpContext.RequestAborted));
        }
    }
}
