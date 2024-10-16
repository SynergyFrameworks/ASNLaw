
using Infrastructure.Common.Caching;
using Infrastructure.Common;

namespace Infrastruture.Caching
{
    /// <summary>
    /// Generic CRUD search cache region implementation for use with crud/search services.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericSearchCachingRegion<T> : CancellableCacheRegion<GenericSearchCachingRegion<T>> where T : Entity
    {
    }
}
