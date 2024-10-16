using System.Threading.Tasks;

namespace Infrastructure.DynamicProperties
{
    public interface IDynamicPropertySearchService
    {
        Task<DynamicPropertySearchResult> SearchDynamicPropertiesAsync(DynamicPropertySearchCriteria criteria);
    }
}
