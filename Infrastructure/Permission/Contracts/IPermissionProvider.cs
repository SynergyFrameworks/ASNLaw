using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Permissions.Contracts
{
    public interface IPermissionProvider
    {
        Task<ICollection<string>> GetPermissions(ClaimsPrincipal user);
    }
}
