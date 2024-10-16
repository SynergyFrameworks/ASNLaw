using Datalayer.Domain;
using Infrastructure.Common.Events;
using System.Collections.Generic;

namespace OrganizationService.Events.Organizaton
{
    public class OrganizationChangedEvent : GenericChangedEntryEvent<OrganizationChangedEvent>
    {
        public OrganizationChangedEvent(IEnumerable<GenericChangedEntry<OrganizationChangedEvent>> changedEntries) : base(changedEntries)
        {
        }
    }
}
