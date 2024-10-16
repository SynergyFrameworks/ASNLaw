using Microsoft.AspNetCore.Authorization;

namespace Scion.Business.Security.Authorization
{
    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        public PermissionAuthorizationRequirement(string permission)
        {
            Permission = permission;
        }
        public string Permission { get; set; }
    }
}
