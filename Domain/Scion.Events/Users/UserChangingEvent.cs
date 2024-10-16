using System.Collections.Generic;
using ASN.Infrastructure.Events;
using ASN.Infrastructure.Security;

namespace ASN.Events.Users
{
    public class UserChangingEvent : GenericChangedEntryEvent<ApplicationUser>
    {
        public UserChangingEvent(IEnumerable<GenericChangedEntry<ApplicationUser>> changedEntries) : base(changedEntries)
        {
        }
    }
}
