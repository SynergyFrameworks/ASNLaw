using System.Threading.Tasks;
using Infrastructure.Common;

namespace Infrastructure.Settings
{
    public interface ISettingsSearchService
    {
        Task<GenericSearchResult<ObjectSettingEntry>> SearchSettingsAsync(SettingsSearchCriteria searchCriteria);
    }
}
