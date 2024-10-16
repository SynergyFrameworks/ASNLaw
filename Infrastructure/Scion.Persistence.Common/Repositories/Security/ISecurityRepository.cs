using System.Linq;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.Security.Model;

namespace Scion.Infrastructure.Security.Repositories
{
    public interface ISecurityRepository : IRepository
    {
        IQueryable<UserApiKeyEntity> UserApiKeys { get; }      
    }
}
