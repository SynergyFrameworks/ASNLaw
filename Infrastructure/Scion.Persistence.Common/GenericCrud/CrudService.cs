using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using Scion.Caching;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.Domain;
using Scion.Infrastructure.Events;
using Scion.Infrastructure.GenericCrud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scion.Data.Common.GenericCrud
{
    /// <summary>
    /// Generic service to simplify CRUD implementation.
    /// To implement the service for applied purpose, inherit your search service from this.
    /// </summary>
    /// <typeparam name="TModel">The type of service layer model</typeparam>
    /// <typeparam name="TEntity">The type of data access layer entity (EF) </typeparam>
    /// <typeparam name="TChangeEvent">The type of *change event</typeparam>
    /// <typeparam name="TChangedEvent">The type of *changed event</typeparam>
    public abstract class CrudService<TModel, TEntity, TChangeEvent, TChangedEvent> : ICrudService<TModel>
        where TModel : Entity, ICloneable
        where TEntity : Entity, IDataEntity<TEntity, TModel>
        where TChangeEvent : GenericChangedEntryEvent<TModel>
        where TChangedEvent : GenericChangedEntryEvent<TModel>
    {
        protected readonly IEventPublisher _eventPublisher;
        protected readonly IPlatformMemoryCache _platformMemoryCache;
        protected readonly Func<IRepository> _repositoryFactory;

        /// <summary>
        /// Construct new CrudService
        /// </summary>
        /// <param name="repositoryFactory">Repository factory to get access to the data source</param>
        /// <param name="platformMemoryCache">The cache used to temporary store returned values</param>
        /// <param name="eventPublisher">The publisher to propagate platform-wide events (TChangeEvent, TChangedEvent)</param>
        protected CrudService(Func<IRepository> repositoryFactory, IPlatformMemoryCache platformMemoryCache, IEventPublisher eventPublisher)
        {
            _repositoryFactory = repositoryFactory;
            _platformMemoryCache = platformMemoryCache;
            _eventPublisher = eventPublisher;
        }

        /// <summary>
        /// Return a model instance for specified id and response group
        /// </summary>
        /// <param name="id"></param>
        /// <param name="responseGroup"></param>
        /// <returns></returns>
        public virtual async Task<TModel> GetByIdAsync(string id, string responseGroup = null)
        {
            IEnumerable<TModel> entities = await GetByIdsAsync(new[] { id }, responseGroup);
            return entities.FirstOrDefault();
        }

        /// <summary>
        /// Return an enumerable set of model instances for specified ids and response group.
        /// Custom CRUD service can override this to implement fully specific read.
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="responseGroup"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TModel>> GetByIdsAsync(IEnumerable<string> ids, string responseGroup = null)
        {
            string cacheKey = CacheKey.With(GetType(), nameof(GetByIdsAsync), string.Join("-", ids), responseGroup);
            List<TModel> result = await _platformMemoryCache.GetOrCreateExclusiveAsync(cacheKey, async (cacheEntry) =>
            {
                List<TModel> models = new List<TModel>();

                using (IRepository repository = _repositoryFactory())
                {
                    //Disable DBContext change tracking for better performance 
                    repository.DisableChangesTracking();

                    //It is so important to generate change tokens for all ids even for not existing objects to prevent an issue
                    //with caching of empty results for non - existing objects that have the infinitive lifetime in the cache
                    //and future unavailability to create objects with these ids.
                    cacheEntry.AddExpirationToken(CreateCacheToken(ids));

                    IEnumerable<TEntity> entities = await LoadEntities(repository, ids, responseGroup);

                    foreach (TEntity entity in entities)
                    {
                        TModel model = entity.ToModel(AbstractTypeFactory<TModel>.TryCreateInstance());
                        model = ProcessModel(responseGroup, entity, model);
                        if (model != null) models.Add(model);
                    }

                }

                return models;
            });

            return result.Select(x => (TModel)x.Clone());
        }

        /// <summary>
        /// Post-read processing of the model instance.
        /// A good place to make some additional actions, tune model data.
        /// Override to add some model data changes, calculations, etc...
        /// </summary>
        /// <param name="responseGroup"></param>
        /// <param name="entity"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected virtual TModel ProcessModel(string responseGroup, TEntity entity, TModel model)
        {
            return model;
        }

        /// <summary>
        /// Custom CRUD service must override this method to implement a call to repository for data read
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="ids"></param>
        /// <param name="responseGroup"></param>
        /// <returns></returns>
        protected abstract Task<IEnumerable<TEntity>> LoadEntities(IRepository repository, IEnumerable<string> ids, string responseGroup);

        /// <summary>
        /// Just calls LoadEntities with "Full" response group
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        protected virtual Task<IEnumerable<TEntity>> LoadEntities(IRepository repository, IEnumerable<string> ids)
        {
            return LoadEntities(repository, ids, "Full");
        }

        /// <summary>
        /// Custom CRUD service can override to implement some actions before save
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        protected virtual Task BeforeSaveChanges(IEnumerable<TModel> models)
        {
            // Basic implementation left empty
            return Task.CompletedTask;
        }

        /// <summary>
        /// Custom CRUD service can override to implement some actions after save
        /// </summary>
        /// <param name="models"></param>
        /// <param name="changedEntries"></param>
        /// <returns></returns>
        protected virtual Task AfterSaveChangesAsync(IEnumerable<TModel> models, IEnumerable<GenericChangedEntry<TModel>> changedEntries)
        {
            // Basic implementation left empty
            return Task.CompletedTask;
        }

        /// <summary>
        /// Custom CRUD service can override to implement a call to the repository for soft delete.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        protected virtual Task SoftDelete(IRepository repository, IEnumerable<string> ids)
        {
            // Basic implementation of soft delete intentionally left empty.
            return Task.CompletedTask;
        }

        /// <summary>
        /// Persists specific set of enumerable model instances to the data source.
        /// Can be overridden to implement full custom save.
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public virtual async Task SaveChangesAsync(IEnumerable<TModel> models)
        {
            PrimaryKeyResolvingMap pkMap = new PrimaryKeyResolvingMap();
            List<GenericChangedEntry<TModel>> changedEntries = new List<GenericChangedEntry<TModel>>();

            await BeforeSaveChanges(models);

            using (IRepository repository = _repositoryFactory())
            {
                IEnumerable<TEntity> dataExistEntities = await LoadEntities(repository, models.Where(x => !x.IsTransient()).Select(x => x.Id));

                foreach (TModel model in models)
                {

                    TEntity originalEntity = dataExistEntities.FirstOrDefault(x => x.Id == model.Id);
                    TEntity modifiedEntity = AbstractTypeFactory<TEntity>.TryCreateInstance().FromModel(model, pkMap);

                    if (originalEntity != null)
                    {
                        // This extension is allow to get around breaking changes is introduced in EF Core 3.0 that leads to throw
                        // Database operation expected to affect 1 row(s) but actually affected 0 row(s) exception when trying to add the new children entities with manually set keys
                        // https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-3.0/breaking-changes#detectchanges-honors-store-generated-key-values
                        repository.TrackModifiedAsAddedForNewChildEntities(originalEntity);

                        changedEntries.Add(new GenericChangedEntry<TModel>(model, originalEntity.ToModel(AbstractTypeFactory<TModel>.TryCreateInstance()), EntryState.Modified));
                        modifiedEntity.Patch(originalEntity);
                        if (originalEntity is IAuditable auditableOriginalEntity)
                        {
                            auditableOriginalEntity.ModifiedDate = DateTime.UtcNow;
                        }
                    }
                    else
                    {
                        repository.Add(modifiedEntity);
                        changedEntries.Add(new GenericChangedEntry<TModel>(model, EntryState.Added));
                    }
                }

                //Raise domain events
                await _eventPublisher.Publish(EventFactory<TChangeEvent>(changedEntries));
                await repository.UnitOfWork.CommitAsync();
            }
            pkMap.ResolvePrimaryKeys();

            ClearCache(models);

            await AfterSaveChangesAsync(models, changedEntries);

            await _eventPublisher.Publish(EventFactory<TChangedEvent>(changedEntries));
        }

        /// <summary>
        /// Delete models, related to specific set of their ids, from the data source.
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="softDelete"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(IEnumerable<string> ids, bool softDelete = false)
        {
            IEnumerable<TModel> models = (await GetByIdsAsync(ids));

            using (IRepository repository = _repositoryFactory())
            {
                //Raise domain events before deletion
                IEnumerable<GenericChangedEntry<TModel>> changedEntries = models.Select(x => new GenericChangedEntry<TModel>(x, EntryState.Deleted));
                await _eventPublisher.Publish(EventFactory<TChangeEvent>(changedEntries));

                if (softDelete)
                {
                    await SoftDelete(repository, ids);
                }
                else
                {
                    PrimaryKeyResolvingMap keyMap = new PrimaryKeyResolvingMap();
                    foreach (TModel model in models)
                    {
                        TEntity entity = AbstractTypeFactory<TEntity>.TryCreateInstance().FromModel(model, keyMap);
                        repository.Remove(entity);
                    }
                    await repository.UnitOfWork.CommitAsync();
                }

                ClearCache(models);

                //Raise domain events after deletion
                await _eventPublisher.Publish(EventFactory<TChangedEvent>(changedEntries));
            }
        }

        /// <summary>
        /// Create cache region.
        /// Default implementation creates <see cref="GenericCrudCachingRegion<TModel>"/>.
        /// Can be overridden to create some different region.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        protected virtual IChangeToken CreateCacheToken(IEnumerable<string> ids)
        {
            return GenericCachingRegion<TModel>.CreateChangeToken(ids);
        }

        /// <summary>
        /// Clear the cache.
        /// Default implementation expires <see cref="GenericSearchCacheRegion<TModel>"/> region and <see cref="GenericCrudCachingRegion<TModel>"/> regions for every entity
        /// Can be overridden to expire different regions/tokens.
        /// </summary>
        /// <param name="models"></param>
        protected virtual void ClearCache(IEnumerable<TModel> models)
        {
            GenericSearchCachingRegion<TModel>.ExpireRegion();

            foreach (TModel model in models)
            {
                GenericCachingRegion<TModel>.ExpireTokenForKey(model.Id);
            }
        }

        protected virtual GenericChangedEntryEvent<TModel> EventFactory<TEvent>(IEnumerable<GenericChangedEntry<TModel>> changedEntries)
        {
            return (GenericChangedEntryEvent<TModel>)typeof(TEvent).GetConstructor(new Type[] { typeof(IEnumerable<GenericChangedEntry<TModel>>) }).Invoke(new object[] { changedEntries });
        }
    }
}
