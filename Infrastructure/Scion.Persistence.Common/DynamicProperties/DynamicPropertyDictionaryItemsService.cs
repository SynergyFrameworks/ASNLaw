using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Scion.Caching;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.DynamicProperties;
using Scion.Data.Common;
using Scion.Data.Common.Model;
using Scion.Data.Common.Repositories;

namespace Scion.Data.CommonDynamicProperties
{
    public class DynamicPropertyDictionaryItemsService : IDynamicPropertyDictionaryItemsService
    {
        private readonly Func<IPlatformRepository> _repositoryFactory;
        private readonly IPlatformMemoryCache _memoryCache;

        public DynamicPropertyDictionaryItemsService(Func<IPlatformRepository> repositoryFactory, IPlatformMemoryCache memoryCache)
        {
            _repositoryFactory = repositoryFactory;
            _memoryCache = memoryCache;
        }

        public virtual async Task<DynamicPropertyDictionaryItem[]> GetDynamicPropertyDictionaryItemsAsync(string[] ids)
        {
            var cacheKey = CacheKey.With(GetType(), "GetDynamicPropertyDictionaryItemsAsync", string.Join("-", ids));
            return await _memoryCache.GetOrCreateExclusiveAsync(cacheKey, async (cacheEntry) =>
            {
                cacheEntry.AddExpirationToken(DynamicPropertiesCache.CreateChangeToken());
                using (var repository = _repositoryFactory())
                {
                    //Optimize performance and CPU usage
                    repository.DisableChangesTracking();

                    var result = await repository.GetDynamicPropertyDictionaryItemByIdsAsync(ids);
                    return result.Select(x => x.ToModel(AbstractTypeFactory<DynamicPropertyDictionaryItem>.TryCreateInstance())).ToArray();
                }
            });
        }

        public virtual async Task SaveDictionaryItemsAsync(DynamicPropertyDictionaryItem[] items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            using (var repository = _repositoryFactory())
            {
                var dbExistItems = await repository.GetDynamicPropertyDictionaryItemByIdsAsync(items.Where(x => !x.IsTransient()).Select(x => x.Id).ToArray());
                foreach (var item in items)
                {
                    var originalEntity = dbExistItems.FirstOrDefault(x => x.Id == item.Id);
                    var modifiedEntity = AbstractTypeFactory<DynamicPropertyDictionaryItemEntity>.TryCreateInstance().FromModel(item);
                    if (originalEntity != null)
                    {
                        modifiedEntity.Patch(originalEntity);
                    }
                    else
                    {
                        repository.Add(modifiedEntity);
                    }
                }
                await repository.UnitOfWork.CommitAsync();

                DynamicPropertiesCache.ExpireRegion();
            }

        }

        public virtual async Task DeleteDictionaryItemsAsync(string[] itemIds)
        {
            if (itemIds == null)
            {
                throw new ArgumentNullException(nameof(itemIds));
            }

            using (var repository = _repositoryFactory())
            {
                var items = repository.DynamicPropertyDictionaryItems
                    .Where(v => itemIds.Contains(v.Id))
                    .ToList();

                foreach (var item in items)
                {
                    repository.Remove(item);
                }

                await repository.UnitOfWork.CommitAsync();

                DynamicPropertiesCache.ExpireRegion();
            }
        }
    }
}
