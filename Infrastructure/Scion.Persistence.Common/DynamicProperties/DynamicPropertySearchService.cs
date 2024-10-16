using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Scion.Data.Common;
using Scion.Data.Common.Repositories;
using Scion.Caching;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.DynamicProperties;
using Scion.Infrastructure.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Scion.Data.CommonDynamicProperties
{
    public class DynamicPropertySearchService : IDynamicPropertySearchService
    {
        private readonly Func<IPlatformRepository> _repositoryFactory;
        private readonly IDynamicPropertyService _dynamicPropertyService;
        private readonly IPlatformMemoryCache _memoryCache;

        public DynamicPropertySearchService(Func<IPlatformRepository> repositoryFactory, IDynamicPropertyService dynamicPropertyService, IPlatformMemoryCache memoryCache)
        {
            _repositoryFactory = repositoryFactory;
            _dynamicPropertyService = dynamicPropertyService;
            _memoryCache = memoryCache;
        }


        #region IDynamicPropertySearchService members


        public virtual async Task<DynamicPropertySearchResult> SearchDynamicPropertiesAsync(DynamicPropertySearchCriteria criteria)
        {
            string cacheKey = CacheKey.With(GetType(), "SearchDynamicPropertiesAsync", criteria.GetCacheKey());
            return await _memoryCache.GetOrCreateExclusiveAsync(cacheKey, async (cacheEntry) =>
            {
                cacheEntry.AddExpirationToken(DynamicPropertiesCache.CreateChangeToken());
                DynamicPropertySearchResult result = AbstractTypeFactory<DynamicPropertySearchResult>.TryCreateInstance();
                using (IPlatformRepository repository = _repositoryFactory())
                {
                    //Optimize performance and CPU usage
                    repository.DisableChangesTracking();

                    IQueryable<Common.Model.DynamicPropertyEntity> query = repository.DynamicProperties;

                    if (!string.IsNullOrEmpty(criteria.ObjectType))
                    {
                        query = query.Where(x => x.ObjectType == criteria.ObjectType);
                    }

                    if (!string.IsNullOrEmpty(criteria.Keyword))
                    {
                        query = query.Where(x => x.Name.Contains(criteria.Keyword));
                    }

                    if (!criteria.ObjectTypes.IsNullOrEmpty())
                    {
                        query = query.Where(m => criteria.ObjectTypes.Contains(m.ObjectType));
                    }

                    System.Collections.Generic.IList<SortInfo> sortInfos = criteria.SortInfos;
                    if (sortInfos.IsNullOrEmpty())
                    {
                        sortInfos = new[] { new SortInfo { SortColumn = "Name" } };
                    }
                    query = query.OrderBySortInfos(sortInfos);
                    result.TotalCount = await query.CountAsync();
                    if (criteria.Take > 0)
                    {
                        System.Collections.Generic.List<string> ids = await query.Skip(criteria.Skip)
                                         .Take(criteria.Take)
                                         .Select(x => x.Id)
                                         .AsNoTracking()
                                         .ToListAsync();

                        DynamicProperty[] properties = await _dynamicPropertyService.GetDynamicPropertiesAsync(ids.ToArray());
                        result.Results = properties.OrderBy(x => ids.IndexOf(x.Id))
                            .ToList();
                    }
                }
                return result;
            });
        }
        #endregion
    }
}
