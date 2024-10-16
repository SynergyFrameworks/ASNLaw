using System.Collections.Generic;

namespace Infrastructure.Security
{
    public interface IPermissionsRegistrar
    {
        void RegisterPermissions(Permission[] permissions);
        IEnumerable<Permission> GetAllPermissions();
    }
}
