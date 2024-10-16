using System.Threading.Tasks;

namespace Infrastructure.Common.Caching
{
    public interface ICacheService

    {
        public Task<string> GetCacheValueAsync(string cacheKey);

        public Task SetCacheValueAsync(string cacheKey, string value, int minutes);

    }
}
