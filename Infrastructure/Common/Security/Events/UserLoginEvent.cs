using Infrastructure.Common.Events;

namespace Infrastructure.Security.Events
{
    public class UserLoginEvent : DomainEvent
    {
        public UserLoginEvent(ApplicationUser user)
        {
            User = user;
        }

        public ApplicationUser User { get; set; }
    }
}
