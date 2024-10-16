using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Common.Caching
{
    public interface IPlatformMemoryCache : IMemoryCache
    {
        MemoryCacheEntryOptions GetDefaultCacheEntryOptions();
    }
}
