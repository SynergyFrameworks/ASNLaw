using Scion.Infrastructure.Security;

namespace Scion.Infrastructure.Web.Middleware
{
    internal class HangfireUserContextMiddleware
    {
        private IUserNameResolver userNameResolver;

        public HangfireUserContextMiddleware(IUserNameResolver userNameResolver)
        {
            this.userNameResolver = userNameResolver;
        }
    }
}