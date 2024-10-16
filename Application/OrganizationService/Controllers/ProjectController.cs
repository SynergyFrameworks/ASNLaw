using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Model;
using Datalayer.Domain.Group;
using Datalayer.Domain;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models;
using Infrastructure.CQRS.Projections;
using Infrastructure.Common.Domain;
using System;
using System.Threading.Tasks;
using static OrganizationService.PermissionNames;

namespace OrganizationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IService<Project, DefaultSearch<ProjectSearchResult>> _ProjectService;
        private readonly IMapper _mapper;
        public ProjectController(IService<Project, DefaultSearch<ProjectSearchResult>> service, IMapper mapper)
        {
            _ProjectService = service;
            _mapper = mapper;
        }


        [HttpGet("{ProjectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ProjectPermissions.CAN_READ)]
        public async Task<IActionResult> Get(Guid ProjectId)
        {
            return Ok(await _ProjectService.Get(ProjectProjection.ProjectInformation, ProjectId));
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ProjectPermissions.CAN_READ)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _ProjectService.Get(ProjectProjection.ProjectInformation, HttpContext.RequestAborted));
        }

        [HttpPost("query")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ProjectPermissions.CAN_READ)]
        public async Task<IActionResult> Query(DefaultSearch<ProjectSearchResult> search)
        {
            return Ok(await _ProjectService.Query(search, HttpContext.RequestAborted));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ProjectPermissions.CAN_CREATE)]
        public async Task<IActionResult> Post(ProjectInfo newProject)
        {
            if (!ModelState.IsValid) return BadRequest();

            Project client = new Project()
            {
                Name = newProject.Name,
                Description = newProject.Description,
                GroupId = newProject.GroupId,
                ImageUrl = newProject.ImageUrl,
                CreatedBy = "system", // Add User Information when claims are added
                CreatedDate = DateTime.UtcNow
            };

            return Ok(await _ProjectService.Add(client));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ProjectPermissions.CAN_WRITE)]
        public async Task<IActionResult> Put(ProjectInfo updateProject)
        {
            if (!ModelState.IsValid && updateProject.Id == Guid.Empty) return BadRequest();

            Project Project = new Project();

            Project.Description = updateProject.Description;
            Project.Name = updateProject.Name;
            Project.GroupId = updateProject.GroupId;
            Project.ImageUrl = updateProject.ImageUrl;
            Project.Id = updateProject.Id;
            Project.ModifiedBy = "system"; // Add User Information when claims are added
            Project.ModifiedDate = DateTime.UtcNow;

            return Ok(await _ProjectService.Update(Project));
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ProjectPermissions.CAN_DELETE)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();
            return Ok(await _ProjectService.Delete(new Project { Id = id, DeletedBy = "system", DeletedDate = DateTime.UtcNow }));
        }
    }
}
