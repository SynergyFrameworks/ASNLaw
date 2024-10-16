using System.Collections.Generic;

namespace Infrastructure.Permissions.Models
{
    public class PermissionOptions
    {
        public PermissionOptions(string permissionName)
        {
            this.PermissionName = permissionName;
        }

        public string PermissionName { get; set; }

        public bool CanCreate { get; set; }
        
        public bool CanDelete { get; set; }

        public bool CanRead { get; set; }
        
        public bool CanWrite { get; set; }

        internal List<string> GeneratePermissions()
        {
            var list = new List<string>();

            if (CanCreate) list.Add($"{PermissionName}.CanCreate");
            if (CanDelete) list.Add($"{PermissionName}.CanDelete");
            if (CanRead) list.Add($"{PermissionName}.CanRead");
            if (CanWrite) list.Add($"{PermissionName}.CanWrite");

            return list;
        }
    }
}
