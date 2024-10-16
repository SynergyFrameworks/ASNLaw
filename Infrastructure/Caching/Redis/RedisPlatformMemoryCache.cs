using Infrastructure.Common.Extensions;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Infrastruture.Caching.Redis
{
    public class RedisPlatformMemoryCache : PlatformMemoryCache
    {
        private readonly ISubscriber _bus;
        private readonly CachingOptions _cachingOptions;
        private readonly RedisCachingOptions _redisCachingOptions;
        private readonly IConnectionMultiplexer _connection;
        private readonly ILogger _log;
        private readonly TelemetryClient _telemetryClient;
        private static ConcurrentDictionary<string, object> _lockLookup = new ConcurrentDictionary<string, object>();
        private bool _disposed;
        private static readonly string _cacheId = Guid.NewGuid().ToString("N");
        private readonly IRedisCacheClient _redisCacheClient;

        public RedisPlatformMemoryCache(IMemoryCache memoryCache
            , IConnectionMultiplexer connection
            , ISubscriber bus
            , IOptions<CachingOptions> cachingOptions
            , IOptions<RedisCachingOptions> redisCachingOptions
            , IRedisCacheClient redisCacheClient
            , ILogger<RedisPlatformMemoryCache> log

            , TelemetryClient telemetryClient
            ) : base(memoryCache, cachingOptions, log)
        {
            _connection = connection;
            _log = log;
            _telemetryClient = telemetryClient;
            _bus = bus;
            _redisCacheClient = redisCacheClient;
            _cachingOptions = cachingOptions.Value;
            _redisCachingOptions = redisCachingOptions.Value;

            connection.ConnectionFailed += OnConnectionFailed;
            connection.ConnectionRestored += OnConnectionRestored;
            var db = connection.GetDatabase();
            _bus.Subscribe(_redisCachingOptions.ChannelName, OnMessage, CommandFlags.FireAndForget);

            _log.LogInformation($"{nameof(RedisPlatformMemoryCache)}: subscribe to channel {_redisCachingOptions.ChannelName} current instance:{_cacheId}");
            _telemetryClient.TrackEvent("RedisSubscribed", new Dictionary<string, string>
            {
                {"channelName", _redisCachingOptions.ChannelName},
                {"cacheId", _cacheId}
            });
        }

        protected virtual void OnConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            _log.LogError($"Redis disconnected from instance {_cacheId}. Endpoint is {e.EndPoint}, failure type is {e.FailureType}");
            _telemetryClient.TrackException(e.Exception);
            _telemetryClient.TrackEvent("RedisDisconnected", new Dictionary<string, string>
            {
                {"channelName", _redisCachingOptions.ChannelName},
                {"cacheId", _cacheId},
                {"endpoint", e.EndPoint.ToString()},
                {"failureType", e.FailureType.ToString()}
            });

            // If we have no connection to Redis, we can't invalidate cache on another platform instances,
            // so the better idea is to disable cache at all for data consistence
            CacheEnabled = false;
            // We should fully clear cache because we don't know
            // what's changed until platform found Redis is unavailable
            GlobalAppCache.ExpireRegion();
        }

        protected virtual void OnConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            _log.LogInformation($"Redis connection restored for instance {_cacheId}");
            _telemetryClient.TrackEvent("RedisConnectionRestored", new Dictionary<string, string>
            {
                {"channelName", _redisCachingOptions.ChannelName},
                {"cacheId", _cacheId}
            });

            // Return cache to the same state as it was initially.
            // Don't set directly true because it may be disabled in app settings
            CacheEnabled = _cachingOptions.CacheEnabled;
            // We should fully clear cache because we don't know
            // what's changed in another instances since Redis became unavailable
            GlobalAppCache.ExpireRegion();
        }


        protected virtual void OnMessage(RedisChannel channel, RedisValue redisValue)
        {
            RedisCachingMessage message = JsonConvert.DeserializeObject<RedisCachingMessage>(redisValue);

            if (!string.IsNullOrEmpty(message.Id) && !message.Id.EqualsInvariant(_cacheId))
            {
                foreach (object item in message.CacheKeys)
                {
                    base.Remove(item);

                    _log.LogInformation($"{nameof(RedisPlatformMemoryCache)}: channel[{_redisCachingOptions.ChannelName}] remove local cache that cache key is {item} from instance:{_cacheId}");
                }
            }
        }

        protected override void EvictionCallback(object key, object value, EvictionReason reason, object state)
        {
            _log.LogInformation($"{nameof(RedisPlatformMemoryCache)}: channel[{_redisCachingOptions.ChannelName}] sending a message with key:{key} from instance:{_cacheId} to all subscribers");

            RedisCachingMessage message = new RedisCachingMessage { Id = _cacheId, CacheKeys = new[] { key } };
            _bus.Publish(_redisCachingOptions.ChannelName, JsonConvert.SerializeObject(message), CommandFlags.FireAndForget);

            base.EvictionCallback(key, value, reason, state);
        }

        //public async Task<TItem> GetOrCreateExclusiveAsyncV<TItem>(string key, TItem CacheItem, bool cacheNullValue = true)
        //{


        //    return await _redisCacheClient.GetDbFromConfiguration().GetAsync<TItem>(key);






        //}



        //await _redisCacheClient.GetDbFromConfiguration().AddAsync<TItem>(key, cacheItem, DateTimeOffset.Now.AddHours(1));



        //    if (true) { }
        //   // return await _redisCacheClient.GetDbFromConfiguration().GetAsync<TItem>(key);

        //   // _redisCacheClient.GetDbFromConfiguration().SearchKeysAsync("*con*")
        //   // List<string> allKeys = (await _redisCacheClient.GetDbFromConfiguration().SearchKeysAsync("*")).ToList();
        //   //await _redisCacheClient.GetDbFromConfiguration().RemoveAsync("test");
        //   //await _redisCacheClient.GetDbFromConfiguration().RemoveAllAsync(allKeys);

        //    return cacheValueReturned;
        //}



        //protected override void Dispose(bool disposing)
        //{
        //    if (!_disposed)
        //    {
        //        if (disposing)
        //        {
        //            _bus.Unsubscribe(_redisCachingOptions.ChannelName, null, CommandFlags.FireAndForget);
        //            _connection.ConnectionFailed -= OnConnectionFailed;
        //            _connection.ConnectionRestored -= OnConnectionRestored;
        //        }
        //        _disposed = true;
        //    }

        //    base.Dispose(disposing);
        //}
    }
}
