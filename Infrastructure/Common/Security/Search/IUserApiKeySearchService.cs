using System.Threading.Tasks;

namespace Infrastructure.Security.Search
{
    public interface IUserApiKeySearchService
    {
        Task<UserApiKeySearchResult> SearchUserApiKeysAsync(UserApiKeySearchCriteria criteria);
    }
}
