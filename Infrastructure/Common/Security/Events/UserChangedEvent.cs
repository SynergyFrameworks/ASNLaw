using System.Collections.Generic;
using Infrastructure.Common.Events;

namespace Infrastructure.Security.Events
{
    public class UserChangedEvent : GenericChangedEntryEvent<ApplicationUser>
    {
        public UserChangedEvent(IEnumerable<GenericChangedEntry<ApplicationUser>> changedEntries) : base(changedEntries)
        {
        }
    }
}
