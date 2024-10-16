using System.Collections.Generic;
using Scion.Infrastructure.Security;

namespace Scion.Business.Security
{
    public class DefaultPermissionProvider : IPermissionsRegistrar
    {
        private readonly List<Permission> _permissions = new List<Permission>();

        public IEnumerable<Permission> GetAllPermissions()
        {
            return _permissions;
        }

        public void RegisterPermissions(params Permission[] permissions)
        {
            _permissions.AddRange(permissions);
        }
    }
}
