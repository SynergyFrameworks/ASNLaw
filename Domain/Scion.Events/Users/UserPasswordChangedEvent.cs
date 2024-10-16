using ASNInfrastructure.Events;

namespace ASNEvents.Users
{
    public class UserPasswordChangedEvent : DomainEvent
    {
        public UserPasswordChangedEvent(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }
    }
}
