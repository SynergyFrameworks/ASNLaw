using Infrastructure.Common.Events;

namespace Infrastructure.Security.Events
{
    public class UserResetPasswordEvent : DomainEvent
    {
        public UserResetPasswordEvent(string userId, string сustomPasswordHash)
        {
            UserId = userId;
            CustomPasswordHash = сustomPasswordHash;
        }

        public string UserId { get; set; }

        /// <summary>
        /// Password hash for external hash storage. This provided as workaround until password hash storage would implemented
        /// </summary>         
        public string CustomPasswordHash { get; set; }
    }
}
