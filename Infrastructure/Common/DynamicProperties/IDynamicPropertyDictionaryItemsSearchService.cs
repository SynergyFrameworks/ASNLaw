using System.Threading.Tasks;

namespace Infrastructure.DynamicProperties
{
    public interface IDynamicPropertyDictionaryItemsSearchService
    {
        Task<DynamicPropertyDictionaryItemSearchResult> SearchDictionaryItemsAsync(DynamicPropertyDictionaryItemSearchCriteria criteria);
    }
}
