using System.Threading.Tasks;

namespace Infrastructure.Common.Assets
{
    public interface IAssetEntrySearchService
    {
        Task<AssetEntrySearchResult> SearchAssetEntriesAsync(AssetEntrySearchCriteria criteria);
    }
}
