
using Microsoft.Extensions.Caching.Distributed;
using Infrastructure.Common.Caching;
using Serilog;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Infrastruture.Caching.Redis
{
    public class RedisMemoryCacheService : ICacheService
    {

        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly ILogger _log;
        private static readonly string _cacheId = Guid.NewGuid().ToString("N");

        public RedisMemoryCacheService(IConnectionMultiplexer connectionMultiplexer, ILogger log)
        {
            _connectionMultiplexer = connectionMultiplexer;
           _log = log;
            _connectionMultiplexer.ConnectionFailed += OnConnectionFailed;
            _connectionMultiplexer.ConnectionRestored += OnConnectionRestored;
    
        }

        public async Task<string> GetCacheValueAsync(string cacheKey)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return await db.StringGetAsync(cacheKey);


        }

        public async Task SetCacheValueAsync(string cacheKey, string value, int timeOutInMinutes)
        {

            var ts = TimeSpan.FromMinutes(timeOutInMinutes);
            var db = _connectionMultiplexer.GetDatabase();
            _ = await db.StringSetAsync(cacheKey, value);

        }


        protected virtual void OnConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            _log.Error($"Redis disconnected from instance { _cacheId }. Endpoint is {e.EndPoint}, failure type is {e.FailureType}");

        }

        protected virtual void OnConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            _log.Information($"Redis connection restored for instance { _cacheId }");

        }



    }
}
