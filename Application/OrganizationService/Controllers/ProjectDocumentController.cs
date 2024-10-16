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
    //TODO: Add the missing permissions (weren't defined in the DB)
    [Route("api/projectdocument")]
    [ApiController]
    [Authorize]
    public class ProjectDocumentController : ControllerBase
    {
        private readonly IService<ProjectDocument, DefaultSearch<ProjectDocument>> _projectDocumentsService;
        public ProjectDocumentController(IService<ProjectDocument, DefaultSearch<ProjectDocument>> projectDocumentsService)
        {
            _projectDocumentsService = projectDocumentsService;
        }


        [HttpGet("{projectdocumentId}")]
        [Authorize(ProjectDocumentPermissions.CAN_READ)]
        public async Task<IActionResult> Get(Guid projectdocumentId)
        {
            return Ok(await _projectDocumentsService.Get(ProjectDocumentProjection.ProjectDocumentInformation, projectdocumentId));
        }

        [HttpGet()]
        [Authorize(ProjectDocumentPermissions.CAN_READ)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _projectDocumentsService.Get(ProjectDocumentProjection.ProjectDocumentInformation, HttpContext.RequestAborted));
        }

        [HttpPost("query")]
        [Authorize(ProjectDocumentPermissions.CAN_READ)]
        public async Task<IActionResult> Query(DefaultSearch<ProjectDocument> search)
        {

            if (!ModelState.IsValid) return BadRequest();
            return Ok(await _projectDocumentsService.Query(search, HttpContext.RequestAborted));
        }

        [HttpPost]
        [Authorize(ProjectDocumentPermissions.CAN_CREATE)]
        public async Task<IActionResult> Post(ProjectDocumentInfo newProjectDocument)
        {
            if (!ModelState.IsValid) return BadRequest();

            ProjectDocument projectDocument = new ProjectDocument()
            {
                Name = newProjectDocument.Name,
                Description = newProjectDocument.Description,
                ProjectId = newProjectDocument.ProjectId,
                DocumentSourceId = newProjectDocument.DocumentSourceId,
                Extension = newProjectDocument.Extension,
                Size = newProjectDocument.Size,
                Url = newProjectDocument.Url,
                IsOutput = newProjectDocument.IsOutput,
                CreatedBy = "system",
                CreatedDate = DateTime.UtcNow
            };

            return Ok(await _projectDocumentsService.Add(projectDocument, HttpContext.RequestAborted));
        }

        [HttpPut]
        [Authorize(ProjectDocumentPermissions.CAN_WRITE)]
        public async Task<IActionResult> Put(ProjectDocumentInfo updateProjectDocument)
        {
            if (!ModelState.IsValid && updateProjectDocument.Id == Guid.Empty) return BadRequest();

            var ProjectDocument = new ProjectDocument();
            ProjectDocument.Id = updateProjectDocument.Id;
            ProjectDocument.Name = updateProjectDocument.Name;
            ProjectDocument.Description = updateProjectDocument.Description;
            ProjectDocument.ProjectId = updateProjectDocument.ProjectId;
            ProjectDocument.DocumentSourceId = updateProjectDocument.DocumentSourceId;
            ProjectDocument.Extension = updateProjectDocument.Extension;
            ProjectDocument.Size = updateProjectDocument.Size;
            ProjectDocument.Url = updateProjectDocument.Url;
            ProjectDocument.IsOutput = updateProjectDocument.IsOutput; 


            ProjectDocument.ModifiedBy = "system";
            ProjectDocument.ModifiedDate = DateTime.UtcNow;

            return Ok(await _projectDocumentsService.Update(ProjectDocument, HttpContext.RequestAborted));
        }

        [HttpDelete]
        [Route("{projectdocumentId}")]
        [Authorize(ProjectDocumentPermissions.CAN_DELETE)]
        public async Task<IActionResult> Delete(Guid projectDocumentId)
        {
            if (projectDocumentId == Guid.Empty) return BadRequest();
            return Ok(await _projectDocumentsService.Delete(new ProjectDocument { Id = projectDocumentId, DeletedBy = "system", DeletedDate = DateTime.UtcNow }, HttpContext.RequestAborted));
        }
    }
}
