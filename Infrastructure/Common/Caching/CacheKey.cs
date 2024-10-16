using System;
using Infrastructure.Common.Extensions;

namespace Infrastructure.Common.Caching
{
    public static class CacheKey 
    {
        public static string With(params string[] keys)
        {
            return string.Join("-", keys);
        }

        public static string With(Type ownerType, params string[] keys)
        {
            return With($"{ownerType.GetCacheKey()}:{string.Join("-", keys)}");
        }

   
    }
}
