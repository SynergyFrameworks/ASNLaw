using Infrastructure.Common.Eventstores;
using System.Threading.Tasks;

namespace Infrastructure.Common.Events
{
    public interface IEventBus
    {
        Task PublishLocal(params IEvent[] events);
        Task Commit(params IEvent[] events);
        Task Commit(StreamState stream);
    }
}
