using OrganizationService.Model;
using Datalayer.Domain.Security;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models;
using Infrastructure.Permissions.Runtime.Client;
using Infrastructure.Permissions.Contracts;
using Infrastructure.Permissions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Infrastructure.Permissions;

namespace OrganizationService.Providers
{
    public class InternalPermissionsProvider : IPermissionProvider
    {
        private const string USER_ID_CLAIM_TYPE = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        private readonly IService<User, DefaultSearch<UserSearchResult>> _userService;

        public InternalPermissionsProvider(IService<User, DefaultSearch<UserSearchResult>> userService)
        {
            _userService = userService;
        }

        public async Task<ICollection<string>> GetPermissions(ClaimsPrincipal user)
        {
            var User = await TryToGetASNUser(user);
            if (User?.Permissions?.Count > 0)
            {
                return GetUserPermissions(User);
            }

            return GetGroupPermissions(User);
        }

        private async Task<User> TryToGetASNUser(ClaimsPrincipal userPrincipal)
        {
            var userId = userPrincipal.FindFirstValue(USER_ID_CLAIM_TYPE);
            if (!string.IsNullOrWhiteSpace(userId))
            {
                return await QueryUserPermissions(Guid.Parse(userId));
            }

            return default;
        }

        private Task<User> QueryUserPermissions(Guid identityId)
        {
            //TODO: Add a command to the user service to get the permissions similiar to this projection.
            return _userService.Query(
                u => new User
                {
                    Permissions = u.Permissions,
                    Groups = u.Groups.Select(
                    g =>
                            new Datalayer.Domain.Group.ASNGroup
                            {
                                GroupPermissions = g.GroupPermissions.Select(
                                    gp => new GroupPermission { Id = gp.Id, Permission = gp.Permission }
                                ).AsParallel().ToList(),
                                Id = g.Id,
                                Name = g.Name
                            }).AsParallel().ToList()
                },
                u => u.IdentityUserId == identityId);
        }

        private List<string> GetUserPermissions(User ASNUser)
        {
            if (ASNUser == null)
                return new List<string>();

            return PermissionsGenerator.Generate(
                   ASNUser.Permissions.Select(
                       p =>
                       new PermissionOptions(p.Name)
                       {
                           CanDelete = p.CanDelete,
                           CanRead = p.CanRead,
                           CanWrite = p.CanWrite,
                           CanCreate = p.CanCreate,
                       })
                   );
        }

        private List<string> GetGroupPermissions(User ASNUser)
        {
            if (ASNUser == null)
                return new List<string>();

            return PermissionsGenerator.Generate(
                  ASNUser.Groups
                  .SelectMany(g => g.GroupPermissions)
                  .Select(gp => new PermissionOptions(gp.Permission.Name)
                  {
                      CanDelete = gp.Permission.CanDelete,
                      CanRead = gp.Permission.CanRead,
                      CanWrite = gp.Permission.CanWrite,
                      CanCreate = gp.Permission.CanCreate,
                  }));
        }
    }
}
