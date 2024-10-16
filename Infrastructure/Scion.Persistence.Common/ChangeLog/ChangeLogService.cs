using Microsoft.Extensions.Caching.Memory;
using Scion.Data.Common;
using Scion.Data.Common.Model;
using Scion.Data.Common.Repositories;
using Scion.Caching;
using Scion.Infrastructure.ChangeLog;
using Scion.Infrastructure.Common;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Scion.Data.Common.ChangeLog
{
    public class ChangeLogService : IChangeLogService, ILastModifiedDateTime
    {
        private readonly Func<IPlatformRepository> _repositoryFactory;
        private readonly IPlatformMemoryCache _memoryCache;

        public ChangeLogService(
            Func<IPlatformRepository> platformRepositoryFactory
            , IPlatformMemoryCache memoryCache)
        {
            _repositoryFactory = platformRepositoryFactory;
            _memoryCache = memoryCache;
        }
        #region ILastModifiedDateTime Members

        public DateTimeOffset LastModified
        {
            get
            {
                string cacheKey = CacheKey.With(GetType(), "LastModifiedDateTime");
                return _memoryCache.GetOrCreateExclusive(cacheKey, (cacheEntry) =>
                {
                    cacheEntry.AddExpirationToken(ChangeLogCacheRegion.CreateChangeToken());
                    return DateTimeOffset.Now;
                });
            }
        }

        public void Reset()
        {
            ChangeLogCacheRegion.ExpireRegion();
        }

        #endregion
        #region IChangeLogService Members
        public async Task<OperationLog[]> GetByIdsAsync(string[] ids)
        {
            using (IPlatformRepository repository = _repositoryFactory())
            {
                repository.DisableChangesTracking();

                OperationLogEntity[] existEntities = await repository.GetOperationLogsByIdsAsync(ids);
                return existEntities.Select(x => x.ToModel(AbstractTypeFactory<OperationLog>.TryCreateInstance())).ToArray();
            }
        }

        public virtual async Task SaveChangesAsync(params OperationLog[] operationLogs)
        {
            if (operationLogs == null)
            {
                throw new ArgumentNullException(nameof(operationLogs));
            }
            PrimaryKeyResolvingMap pkMap = new PrimaryKeyResolvingMap();

            using (IPlatformRepository repository = _repositoryFactory())
            {
                string[] ids = operationLogs.Where(x => !x.IsTransient()).Select(x => x.Id).Distinct().ToArray();
                OperationLogEntity[] existEntities = await repository.GetOperationLogsByIdsAsync(ids);
                foreach (OperationLog operation in operationLogs)
                {
                    OperationLogEntity existsEntity = existEntities.FirstOrDefault(x => x.Id == operation.Id);
                    OperationLogEntity modifiedEntity = AbstractTypeFactory<OperationLogEntity>.TryCreateInstance().FromModel(operation, pkMap);
                    if (existsEntity != null)
                    {
                        modifiedEntity.Patch(existsEntity);
                    }
                    else
                    {
                        repository.Add(modifiedEntity);
                    }
                }
                await repository.UnitOfWork.CommitAsync();
                Reset();
            }
        }

        public virtual async Task DeleteAsync(string[] ids)
        {
            using (IPlatformRepository repository = _repositoryFactory())
            {
                OperationLogEntity[] existEntities = await repository.GetOperationLogsByIdsAsync(ids);
                foreach (OperationLogEntity entity in existEntities)
                {

                    repository.Remove(entity);
                }
                await repository.UnitOfWork.CommitAsync();
                Reset();
            }
        }
        #endregion

    }
}
