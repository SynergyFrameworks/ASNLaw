using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Contracts
{
   public interface ICacheManager
    {
        int CacheExpiration { get; set; }

        void Add(string key, object value);
        object Get(string key);
        void Remove(string key);
        void ResetExpiration(string key);
    }
}
