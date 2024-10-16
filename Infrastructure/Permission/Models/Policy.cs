using Newtonsoft.Json;
using Infrastruture.Caching;
using Infrastruture.Caching.Redis;
using Infrastructure.Permissions.Contracts;
using Infrastructure.Permissions.Runtime.Client;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Infrastructure.Common.Caching;
using Serilog;

namespace Infrastructure.Permissions.Models
{
    public class Policy
    {

        private readonly IConnectionMultiplexer _connectionMultiplexer;
       private readonly ILogger _log;
        private readonly IPermissionProvider _permissionProvider;
        private const string USER_ID_CLAIM_TYPE = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

        private PermissionsResult _permissionsResult { get; set; }

        public Policy(IPermissionProvider permissionProvider, IConnectionMultiplexer memoryCache, Serilog.ILogger log)
        {
            _permissionProvider = permissionProvider;
            _connectionMultiplexer = memoryCache;
            _log = log;
        }
        internal async Task<PermissionsResult> EvaluateAsync(ClaimsPrincipal user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

           string cacheKey = CacheKey.With(GetType(), "GetPermissionsResults", $"{ user.FindFirstValue(USER_ID_CLAIM_TYPE)}");
           var RedisCache = new RedisMemoryCacheService(_connectionMultiplexer, _log);

           var CacheResult =  await RedisCache.GetCacheValueAsync(cacheKey);

            if (CacheResult != null)
            {

                PermissionsResult result = JsonConvert.DeserializeObject<PermissionsResult>(CacheResult);
                _permissionsResult = result;

            } else {

                ICollection<string> permissions = await _permissionProvider.GetPermissions(user);
                PermissionsResult result = new PermissionsResult()
                {
                    Permissions = permissions.Distinct()
                };
               
                           
               await RedisCache.SetCacheValueAsync(cacheKey, JsonConvert.SerializeObject(result), 2);
               _permissionsResult = result;

            }

            return _permissionsResult;


        }




    }
}
