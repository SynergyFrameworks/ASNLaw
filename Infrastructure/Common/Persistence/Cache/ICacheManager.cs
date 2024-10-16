namespace Infrastructure.Common.Persistence.Cache
{
    interface ICacheManager
    {
        int CacheExpiration { get; set; }

        void Add(string key, object value);
        object Get(string key);
        void Remove(string key);
        void ResetExpiration(string key);
    }
}
