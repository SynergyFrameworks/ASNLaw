using Hangfire.Client;
using Hangfire.Server;
using Infrastructure.Security;
using static Infrastructure.Common.ThreadSlotNames;

namespace Hangfire.Middleware
{
    /// <summary>
    /// This class allow to process all HangFire jobs to add user name from identity and save it to
    /// the Thread after job is performing to achieve getting access to user name in background tasks
    /// </summary>
    public class HangfireUserContextMiddleware : IClientFilter, IServerFilter
    {
        private readonly IUserNameResolver _userNameResolver;

        public HangfireUserContextMiddleware(IUserNameResolver userNameResolver)
        {
            _userNameResolver = userNameResolver;
        }

        private string ContextUserName => _userNameResolver.GetCurrentUserName();

        #region IClientFilter Members

        public void OnCreating(CreatingContext filterContext)
        {
            if (ContextUserName is null)
                return;

            filterContext.SetJobParameter(USER_NAME, ContextUserName);
        }

        public void OnCreated(CreatedContext filterContext)
        {
            // Pass
        }

        #endregion IClientFilter Members

        #region IServerFilter Members

        public void OnPerforming(PerformingContext filterContext)
        {
            var userName = filterContext.GetJobParameter<string>(USER_NAME);

            _userNameResolver.SetCurrentUserName(userName);
        }

        public void OnPerformed(PerformedContext filterContext)
        {
            // Pass
        }

        #endregion IServerFilter Members
    }
}
