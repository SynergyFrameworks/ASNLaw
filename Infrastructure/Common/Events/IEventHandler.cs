using MediatR;
using Infrastructure.Messages;

namespace Infrastructure.Common.Events
{
    public interface IEventHandler<in T, CancellationToken> : IHandler<T> where T : IEvent
    { 
    }
}
