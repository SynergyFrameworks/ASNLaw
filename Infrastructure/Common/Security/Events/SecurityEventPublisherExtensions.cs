using Infrastructure.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;



namespace Infrastructure.Security.Events
{
    public static class SecurityEventsPublisherExtensions
    {
        static Dictionary<string, Func<List<GenericChangedEntry<ApplicationUser>>, IEvent>> factoriesMap = new Dictionary<string, Func<List<GenericChangedEntry<ApplicationUser>>, IEvent>>()
        {
            { PlatformConstants.Security.Changes.UserUpdated, f => new UserChangedEvent(f) },
            { PlatformConstants.Security.Changes.UserPasswordChanged, f => new UserPasswordChangedEvent(f.FirstOrDefault()?.NewEntry) }
        };

        public static IEnumerable<IEvent> GenerateSecurityEventsByChanges(this List<GenericChangedEntry<ApplicationUser>> changedEntries)
        {
            var changes = changedEntries.SelectMany(e => e.NewEntry.DetectUserChanges(e.OldEntry));
            foreach (var change in changes)
            {
                if (factoriesMap.TryGetValue(change.Key, out var factory))
                {
                    yield return factory.Invoke(changedEntries);
                }
            }
        }
    }
}
