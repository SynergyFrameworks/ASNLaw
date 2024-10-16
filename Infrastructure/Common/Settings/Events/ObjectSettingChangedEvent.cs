using System.Collections.Generic;
using Infrastructure.Common.Events;

namespace Infrastructure.Settings.Events
{
    public class ObjectSettingChangedEvent : GenericChangedEntryEvent<ObjectSettingEntry>
    {
        public ObjectSettingChangedEvent(IEnumerable<GenericChangedEntry<ObjectSettingEntry>> changedEntries) : base(changedEntries)
        {
        }
    }
}
