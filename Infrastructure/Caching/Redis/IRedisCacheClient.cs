using System.Collections.Generic;
using System.Threading.Tasks;
using System;

public interface IRedisCacheClient
{
    Task<T> GetAsync<T>(string key);
    Task AddAsync<T>(string key, T value, DateTimeOffset expiration);
    Task RemoveAsync(string key);
    Task<IEnumerable<string>> SearchKeysAsync(string pattern);
}
