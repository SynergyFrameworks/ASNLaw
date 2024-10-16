using ASN.Infrastructure.Events;
using ASN.Infrastructure.Security;

namespace ASN.Events.Users
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
