using Microsoft.AspNetCore.Http;
using Infrastructure.Security;
using System.Threading;

namespace ASNSecurity
{
    public class HttpContextUserResolver : IUserNameResolver
    {
        private static readonly AsyncLocal<string> _currentUserName = new AsyncLocal<string>();

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextUserResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserName()
        {
            string result = _currentUserName.Value; //?? UNKNOWN_USERNAME;

            HttpContext context = _httpContextAccessor.HttpContext;
            if (context != null && context.Request != null && context.User != null)
            {
                System.Security.Principal.IIdentity identity = context.User.Identity;
                if (identity != null && identity.IsAuthenticated)
                {
                    result = context.Request.Headers["ScionAnalytics-User-Name"];
                    if (string.IsNullOrEmpty(result))
                    {
                        result = identity.Name;
                    }
                }
            }
            return result;
        }

        public void SetCurrentUserName(string userName)
        {
            _currentUserName.Value = userName;
        }
    }
}
