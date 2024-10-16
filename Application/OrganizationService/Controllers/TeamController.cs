using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Model;
using Datalayer.Domain;
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
    public class TeamController : ControllerBase
    {
        private readonly IService<Team, DefaultSearch<TeamSearchResult>> _teamService;
        private readonly IMapper _mapper;
        public TeamController(IService<Team, DefaultSearch<TeamSearchResult>> service, IMapper mapper)
        {
            _teamService = service;
            _mapper = mapper;
        }


        [HttpGet("{teamId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(TeamPermissions.CAN_READ)]
        public async Task<IActionResult> Get(Guid teamId)
        {
            return Ok(await _teamService.Get(TeamProjection.TeamInformation, teamId));
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(TeamPermissions.CAN_READ)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _teamService.Get(TeamProjection.TeamInformation, HttpContext.RequestAborted));
        }

        [HttpPost("query")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(TeamPermissions.CAN_READ)]
        public async Task<IActionResult> Query([FromQuery] DefaultSearch<TeamSearchResult> search)
        {
            return Ok(await _teamService.Query(search, HttpContext.RequestAborted));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(TeamPermissions.CAN_CREATE)]
        public async Task<IActionResult> Post(TeamInfo newTeam)
        {
            if (!ModelState.IsValid) return BadRequest();

            Team client = new Team()
            {
                ClientId = newTeam.ClientId,
                Name = newTeam.Name,
                Description = newTeam.Description,
                CreatedBy = "system", // Add User Information when claims are added
                CreatedDate = DateTime.UtcNow
            };

            return Ok(await _teamService.Add(client));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(TeamPermissions.CAN_WRITE)]
        public async Task<IActionResult> Put(TeamInfo updateTeam)
        {
            if (!ModelState.IsValid && updateTeam.Id == Guid.Empty) return BadRequest();

            Team team = new Team();

            team.Description = updateTeam.Description;
            team.Name = updateTeam.Name;
            team.ClientId = updateTeam.ClientId;
            team.Id = updateTeam.Id;

            team.ModifiedBy = "system";
            team.ModifiedDate = DateTime.UtcNow;

            return Ok(await _teamService.Update(team));
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(TeamPermissions.CAN_DELETE)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();
            return Ok(await _teamService.Delete(new Team { Id = id, DeletedBy = "system", DeletedDate = DateTime.UtcNow }));
        }
    }
}
