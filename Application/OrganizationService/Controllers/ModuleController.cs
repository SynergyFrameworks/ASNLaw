using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ModuleController : ControllerBase
    {
        private readonly IService<Module, DefaultSearch<ModuleSearchResult>> _ModuleService;
        private readonly IMapper _mapper;
        public ModuleController(IService<Module, DefaultSearch<ModuleSearchResult>> service, IMapper mapper)
        {
            _ModuleService = service;
            _mapper = mapper;
        }


        [HttpGet("{ModuleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ModulePermissions.CAN_READ)]
        public async Task<IActionResult> Get(Guid ModuleId)
        {
            return Ok(await _ModuleService.Get(ModuleProjection.ModuleInformation, ModuleId));
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ModulePermissions.CAN_READ)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _ModuleService.Get(ModuleProjection.ModuleInformation, HttpContext.RequestAborted));
        }

        [HttpPost("query")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ModulePermissions.CAN_READ)]
        public async Task<IActionResult> Query(DefaultSearch<ModuleSearchResult> search)
        {
            return Ok(await _ModuleService.Query(search, HttpContext.RequestAborted));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ModulePermissions.CAN_CREATE)]
        public async Task<IActionResult> Post(ModuleInfo newModule)
        {
            if (!ModelState.IsValid) return BadRequest();

            Module client = new Module()
            {
                Version = newModule.Version,
                VersionTag = newModule.VersionTag,
                Title = newModule.Title,
                Description = newModule.Description,
                LicenseUrl = newModule.LicenseUrl,
                ProjectUrl = newModule.ProjectUrl,
                RequireLicenseAcceptance = newModule.RequireLicenseAcceptance,
                Notes = newModule.Notes,
                Tags = newModule.Tags,
                IsInstalled = newModule.IsInstalled,
                IsRemovable = newModule.IsRemovable,
                CreatedBy = "system", // Add User Information when claims are added
                CreatedDate = DateTime.UtcNow
            };

            return Ok(await _ModuleService.Add(client));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ModulePermissions.CAN_WRITE)]
        public async Task<IActionResult> Put(ModuleInfo updateModule)
        {
            if (!ModelState.IsValid && updateModule.Id == Guid.Empty) return BadRequest();

            Module Module = new Module();

            Module.Version = updateModule.Version;
            Module.VersionTag = updateModule.VersionTag;
            Module.Title = updateModule.Title;
            Module.Description = updateModule.Description;
            Module.LicenseUrl = updateModule.LicenseUrl;
            Module.ProjectUrl = updateModule.ProjectUrl;
            Module.RequireLicenseAcceptance = updateModule.RequireLicenseAcceptance;
            Module.Notes = updateModule.Notes;
            Module.Tags = updateModule.Tags;
            Module.IsInstalled = updateModule.IsInstalled;
            Module.IsRemovable = updateModule.IsRemovable;
            Module.Id = updateModule.Id;
            Module.ModifiedBy = "system";
            Module.ModifiedDate = DateTime.UtcNow;

            return Ok(await _ModuleService.Update(Module));
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(ModulePermissions.CAN_DELETE)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();
            return Ok(await _ModuleService.Delete(new Module { Id = id, DeletedBy = "system", DeletedDate = DateTime.UtcNow }));
        }
    }
}
