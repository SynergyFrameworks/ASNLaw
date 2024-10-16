using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using Newtonsoft.Json;
using Infrastructure.Common.Domain.Users;
using Infrastructure.Common.Persistence.Cache;

namespace Infrastructure.Common.Persistence.Cache
{
    public class CacheManager : ICacheManager
    {
        private static readonly MemoryCache Cache = MemoryCache.Default;

        public int CacheExpiration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public List<T> Get<T>(string type, Criteria criteria)
        {
            var key = GenerateKey(type, criteria);
            return (List<T>) Cache.Get(key);
        }

        public List<T> Get<T>(string key)
        {
            return (List<T>) Cache.Get(key);
        } 

        public void Save<T>(List<T> data, Criteria criteria)
        {
            var key = GenerateKey(typeof(T).Name, criteria);
            Cache.Add(key, data, new CacheItemPolicy());
        }

        public void RemoveCache(string type, Criteria criteria)
        {
            var key = GenerateKey(type, criteria);
            Cache.Remove(key);
        }
        public void ClearCache()
        {
            Cache.Dispose();
        }

        private string GenerateKey(string type, Criteria criteria)
        {
            return type + JsonConvert.SerializeObject(criteria);
        }

        public void Add(string token, User user)
        {
            throw new NotImplementedException();
        }

        public void Add(string key, object value)
        {
            throw new NotImplementedException();
        }

        public object Get(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void ResetExpiration(string key)
        {
            throw new NotImplementedException();
        }
    }
}
