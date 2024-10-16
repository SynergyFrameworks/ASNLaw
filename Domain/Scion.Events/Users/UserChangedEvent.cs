using System.Collections.Generic;
using ASNInfrastructure.Events;
using ASNInfrastructure.Security;

namespace ASNEvents.Users
{
    public class UserChangedEvent : GenericChangedEntryEvent<ApplicationUser>
    {
        public UserChangedEvent(IEnumerable<GenericChangedEntry<ApplicationUser>> changedEntries) : base(changedEntries)
        {
        }
    }
}
