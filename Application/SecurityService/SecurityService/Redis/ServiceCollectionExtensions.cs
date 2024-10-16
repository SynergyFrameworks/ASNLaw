using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Scion.DistributedLock;

namespace Scion.Infrastructure.Web.Redis
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConnectionString = configuration.GetConnectionString("RedisConnectionString");

            if (!string.IsNullOrEmpty(redisConnectionString))
            {
                //var redis = ConnectionMultiplexer.Connect(redisConnectionString);
                //services.AddSingleton<IConnectionMultiplexer>(redis);
                //services.AddSingleton(redis.GetSubscriber());
                //services.AddDataProtection()
                //        .SetApplicationName("Scion.Infrastructure")
                //        .PersistKeysToStackExchangeRedis(redis, "ScionAnalytics-Keys");                
                //services.AddSingleton<IDistributedLockProvider, RedLockDistributedLockProvider>();
            }
            else
            {
                services.AddSingleton<IDistributedLockProvider, NoLockDistributedLockProvider>();
            }

            return services;
        }
        
    }
}
