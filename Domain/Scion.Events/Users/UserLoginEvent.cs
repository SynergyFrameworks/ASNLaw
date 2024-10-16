using ASNInfrastructure.Events;
using ASNInfrastructure.Security;

namespace ASNEvents.Users 
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
