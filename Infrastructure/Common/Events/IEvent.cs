using Infrastructure.Common.Domain.Contracts;
using Infrastructure.Messages;
using System;

namespace Infrastructure.Common.Events
{
    public interface IEvent : IEntity, IMessage
    {
        int Version { get; set; }
        DateTimeOffset TimeStamp { get; set; }
    }
}
