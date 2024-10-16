using Infrastructure.Security;

namespace Infrastructure.Common.Events
{
    public class UserVerificationEmailEvent : DomainEvent
    {
        public ApplicationUser ApplicationUser { get; set; }

        public UserVerificationEmailEvent(ApplicationUser applicationUser)
        {
            ApplicationUser = applicationUser;
        }
    }
}
