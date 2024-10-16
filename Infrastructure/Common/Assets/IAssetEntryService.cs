using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Common.Assets
{
    public interface IAssetEntryService
    {
        Task<IEnumerable<AssetEntry>> GetByIdsAsync(IEnumerable<string> ids);
        Task SaveChangesAsync (IEnumerable<AssetEntry> items);
        Task DeleteAsync (IEnumerable<string> ids);
    }
}
