using Microsoft.EntityFrameworkCore;
using Scion.Data.Common.Model;
using Scion.Infrastructure.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Scion.Data.Common.Repositories
{
    public class PlatformRepository : DbContextRepositoryBase<PlatformDbContext>, IPlatformRepository
    {
        public PlatformRepository(PlatformDbContext dbContext)
            : base(dbContext)
        {
        }

        #region IPlatformRepository Members
        public virtual IQueryable<SettingEntity> Settings => DbContext.Set<SettingEntity>();

        public virtual IQueryable<DynamicPropertyEntity> DynamicProperties => DbContext.Set<DynamicPropertyEntity>();

        public virtual IQueryable<DynamicPropertyDictionaryItemEntity> DynamicPropertyDictionaryItems => DbContext.Set<DynamicPropertyDictionaryItemEntity>();


        public virtual IQueryable<OperationLogEntity> OperationLogs => DbContext.Set<OperationLogEntity>();



        public virtual async Task<DynamicPropertyEntity[]> GetObjectDynamicPropertiesAsync(string[] objectTypes)
        {
            DynamicPropertyEntity[] properties = await DynamicProperties.Include(x => x.DisplayNames)
                                              .OrderBy(x => x.Name)
                                              .Where(x => objectTypes.Contains(x.ObjectType)).ToArrayAsync();
            return properties;
        }

        public virtual async Task<DynamicPropertyDictionaryItemEntity[]> GetDynamicPropertyDictionaryItemByIdsAsync(string[] ids)
        {
            if (ids.IsNullOrEmpty())
            {
                return Array.Empty<DynamicPropertyDictionaryItemEntity>();
            }

            DynamicPropertyDictionaryItemEntity[] retVal = await DynamicPropertyDictionaryItems.Include(x => x.DisplayNames)
                                     .Where(x => ids.Contains(x.Id))
                                     .ToArrayAsync();
            return retVal;
        }

        public virtual async Task<DynamicPropertyEntity[]> GetDynamicPropertiesByIdsAsync(string[] ids)
        {
            if (ids.IsNullOrEmpty())
            {
                return Array.Empty<DynamicPropertyEntity>();
            }

            DynamicPropertyEntity[] retVal = await DynamicProperties.Include(x => x.DisplayNames)
                                          .Where(x => ids.Contains(x.Id))
                                          .OrderBy(x => x.Name)
                                          .ToArrayAsync();
            return retVal;
        }

        public virtual async Task<DynamicPropertyEntity[]> GetDynamicPropertiesForTypesAsync(string[] objectTypes)
        {
            if (objectTypes.IsNullOrEmpty())
            {
                return Array.Empty<DynamicPropertyEntity>();
            }

            DynamicPropertyEntity[] retVal = await DynamicProperties.Include(p => p.DisplayNames)
                                          .Where(p => objectTypes.Contains(p.ObjectType))
                                          .OrderBy(p => p.Name)
                                          .ToArrayAsync();
            return retVal;
        }


        public virtual async Task<SettingEntity[]> GetObjectSettingsByNamesAsync(string[] names, string objectType, string objectId)
        {
            SettingEntity[] result = await Settings.Include(x => x.SettingValues)
                                 .Where(x => x.ObjectId == objectId && x.ObjectType == objectType)
                                 .Where(x => names.Contains(x.Name))
                                 .OrderBy(x => x.Name)
                                 .ToArrayAsync();
            return result;
        }

        public IQueryable<AssetEntryEntity> AssetEntries => DbContext.Set<AssetEntryEntity>();

        public async Task<AssetEntryEntity[]> GetAssetsByIdsAsync(string[] ids)
        {
            if (ids.IsNullOrEmpty())
            {
                return Array.Empty<AssetEntryEntity>();
            }

            return await AssetEntries.Where(x => ids.Contains(x.Id)).ToArrayAsync();
        }

        public async Task<OperationLogEntity[]> GetOperationLogsByIdsAsync(string[] ids)
        {
            if (ids.IsNullOrEmpty())
            {
                return Array.Empty<OperationLogEntity>();
            }

            return await OperationLogs.Where(x => ids.Contains(x.Id)).ToArrayAsync();
        }


        #endregion

    }
}
