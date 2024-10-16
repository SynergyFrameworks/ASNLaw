using ASNInfrastructure.Events;

namespace ASNEvents.Users
{
    public class UserResetPasswordEvent : DomainEvent
    {
        public UserResetPasswordEvent(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }
    }
}
