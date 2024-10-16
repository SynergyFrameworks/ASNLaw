using Datalayer.Context;

namespace Infrastructure.CQRS.Contracts
{
    public interface ICustomCommand<T, TOut>
    {
        Task<TOut> Execute(ASNDbContext context, T model, CancellationToken token = default);
    }
}
