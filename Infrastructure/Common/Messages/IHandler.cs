using System.Threading.Tasks;

namespace Infrastructure.Messages
{
    public interface IHandler<in T> where T : IMessage
    {
        Task Handle(T message);
    }
}
