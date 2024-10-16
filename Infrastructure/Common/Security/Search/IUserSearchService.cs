using System.Threading.Tasks;

namespace Infrastructure.Security.Search
{
    public interface IUserSearchService
    {
        Task<UserSearchResult> SearchUsersAsync(UserSearchCriteria criteria);

    }
}
