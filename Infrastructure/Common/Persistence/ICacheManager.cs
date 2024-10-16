using Infrastructure.Common.Domain.Users;
using System.Collections.Generic;

namespace Infrastructure.Common.Persistence
{
    public interface ICacheManager
    {
        void Save<T>(List<T> data,Criteria criteria);
        List<T> Get<T>(string type,Criteria criteria);
        List<T> Get<T>(string key); 
        void RemoveCache(string type, Criteria criteria);
        void ClearCache();
        void Add(string token, User user);
        void ResetExpiration(string token);
    }
}
