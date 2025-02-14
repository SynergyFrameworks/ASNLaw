using Microsoft.Extensions.Primitives;
using Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Common.Caching;

namespace Infrastruture.Caching
{
    /// <summary>
    /// Generic CRUD caching region implementation for use with crud services.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericCachingRegion<T> : CancellableCacheRegion<GenericCachingRegion<T>> where T : Entity
    {
        public static IChangeToken CreateChangeToken(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            return CreateChangeToken((IEnumerable<T>)entities.Select(x => x.Id));
        }

        public static IChangeToken CreateChangeToken(IEnumerable<string> entityIds)
        {
            if (entityIds == null)
            {
                throw new ArgumentNullException(nameof(entityIds));
            }

            List<IChangeToken> changeTokens = new List<IChangeToken> { CreateChangeToken() };
            foreach (string entityId in entityIds)
            {
                changeTokens.Add(CreateChangeTokenForKey(entityId));
            }
            return new CompositeChangeToken(changeTokens);
        }
    }
}
