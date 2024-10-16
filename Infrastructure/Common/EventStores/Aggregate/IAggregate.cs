using Infrastructure.Common.Events;
using System;
using System.Collections.Generic;

namespace Infrastructure.Common.Eventstores.Aggregate
{
    public interface IAggregate
    {
        Guid Id { get; }
        int Version { get; }
        DateTime CreatedUtc { get; }

        IEnumerable<IEvent> DequeueUncommittedEvents();

    }
}
