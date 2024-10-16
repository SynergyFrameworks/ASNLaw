using System;
using Infrastructure.Common;

namespace Infrastructure.Common.Events
{
    public class DomainEvent : Entity, IEvent
    {
        public DomainEvent()
        {
            Id = Guid.NewGuid();
            TimeStamp = DateTime.UtcNow;
        }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        
        public Guid Id { get; set; }
    }
}
