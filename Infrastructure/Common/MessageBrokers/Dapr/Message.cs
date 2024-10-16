using Infrastructure.Common.Events;
using System;

namespace Infrastructure.Common.MessageBrokers.Dapr
{
    public class Message
    {
        public Guid Id { get; set; }
        public IEvent Content { get; set; }
        public string Subject { get; set; }
    }
}
