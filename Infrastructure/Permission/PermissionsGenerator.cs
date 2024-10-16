using System.Collections.Generic;

namespace Infrastructure.Permissions
{
    //TODO: Take this out from here... should be on the service itself.
    public static class PermissionsGenerator
    {
        public static List<string> Generate(IEnumerable<Models.PermissionOptions> permissionOptions) {
            List<string> generatedPermissions = new List<string>();
            foreach (var permissionOption in permissionOptions)
            {
                generatedPermissions.AddRange(permissionOption.GeneratePermissions());
            }

            return generatedPermissions;
        }
    }
}
