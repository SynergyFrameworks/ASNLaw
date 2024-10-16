using System;
using Infrastructure.Common;

namespace Infrastructure.Common.Events
{
    public abstract class Event : IEvent
    {
     
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public Guid Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
