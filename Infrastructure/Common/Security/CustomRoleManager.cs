using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Infrastructure.Common.Caching;
using Infrastructure.Common;
using Infrastructure.Common.Extensions;
using Infrastructure.Security.Caching;


namespace Infrastructure.Security
{
    public class CustomRoleManager : AspNetRoleManager<Role>
    {
        private readonly IPermissionsRegistrar _knownPermissions;
        private readonly IPlatformMemoryCache _memoryCache;
        private readonly MvcNewtonsoftJsonOptions _jsonOptions;
        public CustomRoleManager(
            IPermissionsRegistrar knownPermissions
            , IPlatformMemoryCache memoryCache
            , IRoleStore<Role> store
            , IEnumerable<IRoleValidator<Role>> roleValidators
            , ILookupNormalizer keyNormalizer
            , IdentityErrorDescriber errors
            , ILogger<RoleManager<Role>> logger
            , IHttpContextAccessor contextAccessor
            , IOptions<MvcNewtonsoftJsonOptions> jsonOptions)
            : base(store, roleValidators, keyNormalizer, errors, logger, contextAccessor)
        {
            _knownPermissions = knownPermissions;
            _memoryCache = memoryCache;
            _jsonOptions = jsonOptions.Value;
        }

        public override async Task<Role> FindByNameAsync(string roleName)
        {
            var cacheKey = CacheKey.With(GetType(), "FindByNameAsync", roleName);
            var result = await _memoryCache.GetOrCreateExclusiveAsync(cacheKey, async (cacheEntry) =>
            {
                cacheEntry.AddExpirationToken(SecurityCache.CreateChangeToken());
                var role = await base.FindByNameAsync(roleName);
                if (role != null)
                {
                    await LoadRolePermissionsAsync(role);
                }
                return role;
            }, cacheNullValue: false);
            return result;
        }

        public override async Task<Role> FindByIdAsync(string roleId)
        {
            var cacheKey = CacheKey.With(GetType(), "FindByIdAsync", roleId);
            var result = await _memoryCache.GetOrCreateExclusiveAsync(cacheKey, async (cacheEntry) =>
            {
                cacheEntry.AddExpirationToken(SecurityCache.CreateChangeToken());
                var role = await base.FindByIdAsync(roleId);
                if (role != null)
                {
                    await LoadRolePermissionsAsync(role);
                }
                return role;
            }, cacheNullValue: false);
            return result;
        }

        public override async Task<IdentityResult> CreateAsync(Role role)
        {
            var result = await base.CreateAsync(role);
            if (result.Succeeded && !role.Permissions.IsNullOrEmpty())
            {
                var existRole = string.IsNullOrEmpty(role.Id) ?  await base.FindByNameAsync(role.Name) : await base.FindByIdAsync(role.Id);
                var permissionRoleClaims = role.Permissions.Select(x => new Claim(PlatformConstants.Security.Claims.PermissionClaimType, x.Name));
                foreach (var claim in permissionRoleClaims)
                {
                    //Need to use an existing tracked by EF entity in order to add permissions for role
                    await base.AddClaimAsync(existRole, claim);
                }
                SecurityCache.ExpireRegion();
            }
            return result;
        }

        public override async Task<IdentityResult> UpdateAsync(Role updateRole)
        {
            if (updateRole == null)
            {
                throw new ArgumentNullException(nameof(updateRole));
            }
            Role existRole = null;
            if (!string.IsNullOrEmpty(updateRole.Id))
            {
                existRole = await base.FindByIdAsync(updateRole.Id);
            }
            if (existRole == null)
            {
                existRole = await base.FindByNameAsync(updateRole.Name);
            }
            if (existRole != null)
            {
                //Need to path exists tracked by EF  entity due to already being tracked exception 
                //https://github.com/aspnet/Identity/issues/1807
                updateRole.Patch(existRole);
            }
            var result = await base.UpdateAsync(existRole);
            if (result.Succeeded && updateRole.Permissions != null)
            {
                var sourcePermissionClaims = updateRole.Permissions.Select(x => x.ToClaim(_jsonOptions.SerializerSettings)).ToList();
                var targetPermissionClaims = (await GetClaimsAsync(existRole)).Where(x => x.Type == PlatformConstants.Security.Claims.PermissionClaimType).ToList();
                var comparer = AnonymousComparer.Create((Claim x) => x.Value);
                //Add
                foreach (var sourceClaim in sourcePermissionClaims.Except(targetPermissionClaims, comparer))
                {
                    await base.AddClaimAsync(existRole, sourceClaim);
                }
                //Remove
                foreach (var targetClaim in targetPermissionClaims.Except(sourcePermissionClaims, comparer).ToArray())
                {
                    await base.RemoveClaimAsync(existRole, targetClaim);
                }
                SecurityCache.ExpireRegion();
            }
            return result;
        }

        public override async Task<IdentityResult> DeleteAsync(Role role)
        {
            var result = await base.DeleteAsync(role);
            if (result.Succeeded)
            {
                SecurityCache.ExpireRegion();
            }
            return result;
        }

        protected virtual async Task LoadRolePermissionsAsync(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            if (SupportsRoleClaims)
            {
                role.Permissions = new List<Permission>();
                //Load role claims and convert it to the permissions and assign to role
                var storedPermissions = (await GetClaimsAsync(role)).Select(x => Permission.TryCreateFromClaim(x, _jsonOptions.SerializerSettings)).ToList();
                var knownPermissionsDict = _knownPermissions.GetAllPermissions().Select(x => x.Clone() as Permission).ToDictionary(x => x.Name, x => x).WithDefaultValue(null);
                foreach (var storedPermission in storedPermissions)
                {
                    //Copy all meta information from registered to stored (for particular role) permission
                    var knownPermission = knownPermissionsDict[storedPermission.Name];
                    if (knownPermission != null)
                    {
                        knownPermission.Patch(storedPermission);
                    }
                    role.Permissions.Add(storedPermission);
                }
            }
        }
    }
}
