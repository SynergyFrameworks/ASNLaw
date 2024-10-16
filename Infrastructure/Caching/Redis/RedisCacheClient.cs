using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.Linq;

public class RedisCacheClient : IRedisCacheClient
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    public RedisCacheClient(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }

    public async Task<T> GetAsync<T>(string key)
    {
        var db = _connectionMultiplexer.GetDatabase();
        var value = await db.StringGetAsync(key);
        return value.IsNullOrEmpty ? default : JsonConvert.DeserializeObject<T>(value);
    }

    public async Task AddAsync<T>(string key, T value, DateTimeOffset expiration)
    {
        var db = _connectionMultiplexer.GetDatabase();
        var serializedValue = JsonConvert.SerializeObject(value);
        await db.StringSetAsync(key, serializedValue, expiration - DateTimeOffset.Now);
    }

    public async Task RemoveAsync(string key)
    {
        var db = _connectionMultiplexer.GetDatabase();
        await db.KeyDeleteAsync(key);
    }

    public async Task<IEnumerable<string>> SearchKeysAsync(string pattern)
    {
        var server = _connectionMultiplexer.GetServer(_connectionMultiplexer.GetEndPoints()[0]);
        return server.Keys(pattern: pattern).Select(k => k.ToString());
    }
}
 