
using Infrastructure.Common.Caching;
using Infrastructure.Security.Caching;

namespace Security.Common.Caching
{
    public class ApiKeyCache : CancellableCache<SecurityCache>
    {
    }
}
