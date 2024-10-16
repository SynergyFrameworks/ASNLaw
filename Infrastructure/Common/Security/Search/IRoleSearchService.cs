using System.Threading.Tasks;

namespace Infrastructure.Security.Search
{
    public interface IRoleSearchService
    {
        Task<RoleSearchResult> SearchRolesAsync(RoleSearchCriteria criteria);
    }
}
