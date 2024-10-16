using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationService.Model;
using Datalayer.Domain.Group;
using Datalayer.Domain;
using Datalayer.Domain.Security;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models;
using Infrastructure.CQRS.Projections;
using System;
using System.Linq;
using System.Threading.Tasks;
using static OrganizationService.PermissionNames;

namespace OrganizationService.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IService<User, DefaultSearch<UserSearchResult>> _UserService;
        private readonly IMapper _mapper;
        public UserController(IService<User, DefaultSearch<UserSearchResult>> service, IMapper mapper)
        {
            _UserService = service;
            _mapper = mapper;
        }


        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(UserPermissions.CAN_READ)]
        public async Task<IActionResult> Get(Guid userId)
        {
            return Ok(await _UserService.Get(UserProjection.UserInformation, userId));
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(UserPermissions.CAN_READ)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _UserService.Get(UserProjection.UserInformation, HttpContext.RequestAborted));
        }

        [HttpPost("query")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(UserPermissions.CAN_READ)]
        public async Task<IActionResult> Query(DefaultSearch<UserSearchResult> search)
        {
            return Ok(await _UserService.Query(search, HttpContext.RequestAborted));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(UserPermissions.CAN_CREATE)]
        public async Task<IActionResult> Post(ASNUserInfo newUser)
        {
            if (!ModelState.IsValid) return BadRequest();

            User user = new User()
            {
                IdentityUserId = newUser.IdentityUserId,
                UserName = newUser.UserName,
                Name = newUser.Name,
                Email = newUser.Email,
                ImageUrl = newUser.ImageUrl,
                IsActive = newUser.IsActive,
                CreatedBy = "system", // Add User Information when claims are added
                CreatedDate = DateTime.UtcNow,
                Clients = newUser.Clients?.Select(clientId => new ASNClient { Id = clientId }).ToList(),
                Groups = newUser.Groups?.Select(groupId => new ASNGroup { Id = groupId }).ToList(),
            };

            return Ok(await _UserService.Add(user));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(UserPermissions.CAN_WRITE)]
        public async Task<IActionResult> Put(ASNUserInfo updateUser)
        {
            if (!ModelState.IsValid && updateUser.Id == Guid.Empty) return BadRequest();

            User user = new User();
            user.IdentityUserId = updateUser.IdentityUserId;
            user.UserName = updateUser.UserName;
            user.Name = updateUser.Name;
            user.Email = updateUser.Email;
            user.ImageUrl = updateUser.ImageUrl;
            user.IsActive = updateUser.IsActive;
            user.Id = updateUser.Id;
            user.ModifiedBy = "system";
            user.ModifiedDate = DateTime.UtcNow;
            user.Clients = updateUser.Clients?.Select(clientId => new ASNClient { Id = clientId }).ToList();
            user.Groups = updateUser.Groups?.Select(groupId => new ASNGroup { Id = groupId }).ToList();

            return Ok(await _UserService.Update(user));
        }
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(UserPermissions.CAN_DELETE)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();
            return Ok(await _UserService.Delete(new User { Id = id, DeletedBy = "system", DeletedDate = DateTime.UtcNow }));
        }
    }
}
