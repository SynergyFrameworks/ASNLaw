using Infrastructure.Common.Events;

namespace Infrastructure.Security.Events
{
    public class UserLogoutEvent : DomainEvent
    {
        public UserLogoutEvent(ApplicationUser user)
        {
            User = user;
        }

        public ApplicationUser User { get; set; }
    }
}
