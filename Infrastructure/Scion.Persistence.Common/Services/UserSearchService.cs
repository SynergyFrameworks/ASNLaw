using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.Extensions;
using Scion.Infrastructure.Security;
using Scion.Infrastructure.Security.Search;

namespace Scion.Infrastructure.Security.Services
{
    public class UserSearchService : IUserSearchService
    {
        private readonly Func<UserManager<ApplicationUser>> _userManagerFactory;

        public UserSearchService(Func<UserManager<ApplicationUser>> userManager, Func<RoleManager<Role>> func)
        {
            _userManagerFactory = userManager;
        }

        public async Task<UserSearchResult> SearchUsersAsync(UserSearchCriteria criteria)
        {
            using (var userManager = _userManagerFactory())
            {
                if (criteria == null)
                {
                    throw new ArgumentNullException(nameof(criteria));
                }
                if (!userManager.SupportsQueryableUsers)
                {
                    throw new NotSupportedException();
                }

                var result = AbstractTypeFactory<UserSearchResult>.TryCreateInstance();
                var query = userManager.Users;
                if (criteria.Keyword != null)
                {
                    query = query.Where(x => x.UserName.Contains(criteria.Keyword));
                }

                if (!string.IsNullOrEmpty(criteria.MemberId))
                {
                    query = query.Where(x => x.MemberId == criteria.MemberId);
                }

                if (!criteria.MemberIds.IsNullOrEmpty())
                {
                    query = query.Where(x => criteria.MemberIds.Contains(x.MemberId));
                }

                if (criteria.ModifiedSinceDate != null && criteria.ModifiedSinceDate != default(DateTime))
                {
                    query = query.Where(x => x.ModifiedDate > criteria.ModifiedSinceDate);
                }

                result.TotalCount = await query.CountAsync();

                var sortInfos = criteria.SortInfos;
                if (sortInfos.IsNullOrEmpty())
                {
                    sortInfos = new[] { new SortInfo { SortColumn = ReflectionUtility.GetPropertyName<ApplicationUser>(x => x.UserName), SortDirection = SortDirection.Descending } };
                }
                result.Results = await query.OrderBySortInfos(sortInfos).Skip(criteria.Skip).Take(criteria.Take).ToArrayAsync();

                return result;
            }
        }
    }
}
