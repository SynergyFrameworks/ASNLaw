using MediatR;

namespace Infrastructure.Common.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    { }
}
