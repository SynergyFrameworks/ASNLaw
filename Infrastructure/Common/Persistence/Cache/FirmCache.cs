using System;
using System.Runtime.Caching;

namespace Infrastructure.Common.Persistence.Cache
{
    public static class FirmCache
    {
        private static readonly MemoryCache Cache = MemoryCache.Default;

        public static void AddItemFixed(string key, object value, DateTimeOffset duration)
        {
            if (value == null)
                return;
            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = duration
            };
            Cache.Add(key, value, policy);
        }
        public static void AddItemSliding(string key, object value, TimeSpan duration)
        {
            if (value == null)
                return;
            var policy = new CacheItemPolicy
            {
                SlidingExpiration = duration
            };
            Cache.Add(key, value, policy);
        }

        public static object GetItem(string key)
        {
            return Cache.Get(key);
        }

        public static void RemoveItem(string key)
        {
            Cache.Remove(key);
        }
    }
}
