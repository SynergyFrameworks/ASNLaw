using System.Linq;
using System.Threading.Tasks;
using Scion.Infrastructure.Common;
using Scion.Data.Common.Model;

namespace Scion.Data.Common.Repositories
{
    public interface IPlatformRepository : IRepository
    {
        IQueryable<AssetEntryEntity> AssetEntries { get; }
        IQueryable<SettingEntity> Settings { get; }

        IQueryable<DynamicPropertyEntity> DynamicProperties { get; }
        IQueryable<DynamicPropertyDictionaryItemEntity> DynamicPropertyDictionaryItems { get; }
        //IQueryable<DynamicPropertyObjectValueEntity> DynamicPropertyObjectValues { get; }
        IQueryable<OperationLogEntity> OperationLogs { get; }

        Task<DynamicPropertyDictionaryItemEntity[]> GetDynamicPropertyDictionaryItemByIdsAsync(string[] ids);
        Task<DynamicPropertyEntity[]> GetDynamicPropertiesForTypesAsync(string[] objectTypes);
        Task<DynamicPropertyEntity[]> GetDynamicPropertiesByIdsAsync(string[] ids);
        Task<DynamicPropertyEntity[]> GetObjectDynamicPropertiesAsync(string[] objectTypes);

        Task<SettingEntity[]> GetObjectSettingsByNamesAsync(string[] names, string objectType, string objectId);

        Task<AssetEntryEntity[]> GetAssetsByIdsAsync(string[] ids);

        Task<OperationLogEntity[]> GetOperationLogsByIdsAsync(string[] ids);
    }
}
