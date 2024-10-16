using Microsoft.Extensions.Primitives;
using Infrastructure.Common.Caching;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Infrastructure.Security.Caching
{
    public class SecurityCache : CancellableCache<SecurityCache>
    {
        private static readonly ConcurrentDictionary<string, CancellationTokenSource> _usersRegionTokenLookup = new ConcurrentDictionary<string, CancellationTokenSource>();

        public static IChangeToken CreateChangeTokenForUser(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            CancellationTokenSource cancellationTokenSource = _usersRegionTokenLookup.GetOrAdd(user.Id, new CancellationTokenSource());
            return new CompositeChangeToken(new[] { CreateChangeToken(), new CancellationChangeToken(cancellationTokenSource.Token) });
        }

        public static void ExpireUser(ApplicationUser user)
        {
            if (_usersRegionTokenLookup.TryRemove(user.Id, out CancellationTokenSource token))
            {
                token.Cancel();
            }
        }
    }
}
