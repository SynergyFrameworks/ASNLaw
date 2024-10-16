using System;
using Microsoft.Extensions.Primitives;
using Scion.Caching;
using Scion.Infrastructure.Security;

namespace Scion.Business.Security.Caching
{
    public class SecurityCacheRegion : CancellableCacheRegion<SecurityCacheRegion>
    {
        public static IChangeToken CreateChangeTokenForUser(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return CreateChangeTokenForKey(user.Id);
        }

        public static void ExpireUser(ApplicationUser user)
        {
            if (user != null)
            {
                ExpireTokenForKey(user.Id);
            }
        }
    }
}
