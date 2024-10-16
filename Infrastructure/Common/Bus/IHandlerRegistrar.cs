using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Messages;

namespace Infrastructure.Common.Bus
{
    public interface IHandlerRegistrar
    {
        void RegisterHandler<T>(Func<T, CancellationToken,Task> handler) where T : class, IMessage;
    }
}
