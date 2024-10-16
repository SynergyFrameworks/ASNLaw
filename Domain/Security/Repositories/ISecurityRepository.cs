using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scion.Infrastructure.Common;
using Scion.Business.Security.Model;

namespace Scion.Business.Security.Repositories
{
    public interface ISecurityRepository : IRepository
    {
        IQueryable<UserApiKeyEntity> UserApiKeys { get; }

        IQueryable<UserPasswordHistoryEntity> UserPasswordsHistory { get; }

        Task<IEnumerable<UserPasswordHistoryEntity>> GetUserPasswordsHistoryAsync(string userId, int passwordsCountToCheck);
    }
}
