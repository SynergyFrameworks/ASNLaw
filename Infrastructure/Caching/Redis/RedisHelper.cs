using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruture.Caching.Redis
{
    public class RedisHelper
    {
        readonly private IConnectionMultiplexer connectionMultiplexer;

        //private IConnectionMultiplexer GetConnectionMultiplexer()
        //{
        //    //return connectionMultiplexer = connectionMultiplexer ??
        //    //                               transientFaultStrategy.Try(
        //    //                                   () => StackExchange.Redis.ConnectionMultiplexer.Connect(
        //    //                                       RedisConnectionString));
        //}


    }
}
