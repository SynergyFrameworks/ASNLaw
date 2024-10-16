using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.Extensions;
using Scion.Infrastructure.Security.Search;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Scion.Infrastructure.Security.Services
{
    public class RoleSearchService : IRoleSearchService
    {
        private readonly RoleManager<Role> _roleManager;
        public RoleSearchService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<RoleSearchResult> SearchRolesAsync(RoleSearchCriteria criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException(nameof(criteria));
            }
            if (!_roleManager.SupportsQueryableRoles)
            {
                throw new NotSupportedException();
            }
            RoleSearchResult result = AbstractTypeFactory<RoleSearchResult>.TryCreateInstance();
            IQueryable<Role> query = _roleManager.Roles;
            if (criteria.Keyword != null)
            {
                query = query.Where(r => r.Name.Contains(criteria.Keyword));
            }
            result.TotalCount = await query.CountAsync();

            System.Collections.Generic.IList<SortInfo> sortInfos = criteria.SortInfos;
            if (sortInfos.IsNullOrEmpty())
            {
                sortInfos = new[] { new SortInfo { SortColumn = ReflectionUtility.GetPropertyName<Role>(x => x.Name), SortDirection = SortDirection.Descending } };
            }
            result.Results = await query.OrderBySortInfos(sortInfos).Skip(criteria.Skip).Take(criteria.Take).ToArrayAsync();

            return result;
        }
    }
}
