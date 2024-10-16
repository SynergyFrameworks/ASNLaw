using Infrastructure.Messages;

namespace Infrastructure.Common.Events
{
    public interface ICancellableEventHandler<in T> : ICancellableHandler<T> where T : IEvent
    {
    }
}
