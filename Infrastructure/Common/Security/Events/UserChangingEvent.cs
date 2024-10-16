using System.Collections.Generic;
using Infrastructure.Common.Events;

namespace Infrastructure.Security.Events
{
    public class UserChangingEvent : GenericChangedEntryEvent<ApplicationUser>
    {
        public UserChangingEvent(IEnumerable<GenericChangedEntry<ApplicationUser>> changedEntries) : base(changedEntries)
        {
        }
    }
}
