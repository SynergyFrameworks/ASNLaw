using System.Linq;
using System.Threading.Tasks;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.Extensions;
using Scion.Infrastructure.Settings;

namespace Scion.Data.Common.Settings
{
    public class SettingsSearchService : ISettingsSearchService
    {
        private readonly ISettingsManager _settingsManager;

        public SettingsSearchService(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }
        public async Task<GenericSearchResult<ObjectSettingEntry>> SearchSettingsAsync(SettingsSearchCriteria criteria)
        {
            var result = new GenericSearchResult<ObjectSettingEntry>();

            var query = _settingsManager.AllRegisteredSettings.AsQueryable();

            if (!string.IsNullOrEmpty(criteria.ModuleId))
            {
                query = query.Where(x => x.ModuleId == criteria.ModuleId);
            }

            var sortInfos = criteria.SortInfos;
            if (sortInfos.IsNullOrEmpty())
            {
                sortInfos = new[] { new SortInfo { SortColumn = "Name" } };
            }
            query = query.OrderBySortInfos(sortInfos);
            result.TotalCount = query.Count();
            var names = query.Skip(criteria.Skip).Take(criteria.Take).Select(x => x.Name).ToList();

            var settings = await _settingsManager.GetObjectSettingsAsync(names.ToArray());
            result.Results = settings.OrderBy(x => names.IndexOf(x.Name))
                                       .ToList();
            return result;
        }
    }
}
