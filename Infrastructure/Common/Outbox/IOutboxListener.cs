using Infrastructure.Common.Events;
using System.Threading.Tasks;

namespace Infrastructure.Common.Outbox
{
    public interface IOutboxListener
    {
        Task Commit(OutboxMessage message);
        Task Commit<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
