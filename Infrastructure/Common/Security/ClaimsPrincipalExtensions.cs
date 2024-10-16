using Newtonsoft.Json;
using Infrastructure.Common.Extensions;
using System.Security.Claims;

namespace Infrastructure.Security
{
    public static class ClaimsPrincipalExtensions
    {
        public static Permission FindPermission(this ClaimsPrincipal principal, string permissionName, JsonSerializerSettings jsonSettings)
        {
            foreach (Claim claim in principal.Claims)
            {
                Permission permission = Permission.TryCreateFromClaim(claim, jsonSettings);
                if (permission != null && permission.Name.EqualsInvariant(permissionName))
                {
                    return permission;
                }
            }
            return null;
        }

        public static bool HasGlobalPermission(this ClaimsPrincipal principal, string permissionName)
        {
            // TODO: Check cases with locked user
            bool result = principal.IsInRole(PlatformConstants.Security.SystemRoles.Administrator);

            if (!result)
            {
                // Breaking change in v3:
                // Do not allow users with Customer role login into platform
                result = !principal.IsInRole(PlatformConstants.Security.SystemRoles.Customer);
                if (result)
                {
                    result = principal.HasClaim(PlatformConstants.Security.Claims.PermissionClaimType, permissionName);
                }
            }
            return result;
        }
    }
}
