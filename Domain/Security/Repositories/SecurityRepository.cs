using Microsoft.EntityFrameworkCore;
using Scion.Business.Security.Model;
using Scion.Infrastructure.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scion.Data.Common;

namespace Scion.Business.Security.Repositories
{
    public class SecurityRepository : DbContextRepositoryBase<SecurityDbContext>, ISecurityRepository
    {
        public SecurityRepository(SecurityDbContext dbContext)
       : base(dbContext)
        {
        }

        public virtual IQueryable<UserApiKeyEntity> UserApiKeys => DbContext.Set<UserApiKeyEntity>();

        public virtual IQueryable<UserPasswordHistoryEntity> UserPasswordsHistory => DbContext.Set<UserPasswordHistoryEntity>();

        public async Task<IEnumerable<UserPasswordHistoryEntity>> GetUserPasswordsHistoryAsync(string userId, int passwordsCountToCheck)
        {
            return await UserPasswordsHistory
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedDate)
                .Take(passwordsCountToCheck)
                .ToArrayAsync();
        }
    }
}
