using System.Linq.Expressions;

namespace Infrastructure.CQRS.Contracts
{
    public interface IService<T, TSearchModel> where T : class where TSearchModel : class
    {
        Task<T> Get(Expression<Func<T, T>> projection, Guid id, CancellationToken cancellationToken = default);
        Task<IList<T>> Get(Expression<Func<T, T>> projection, CancellationToken cancellationToken = default);

        Task<TSearchModel> Query(TSearchModel search, CancellationToken cancellationToken = default);

        Task<T> Query(Expression<Func<T, T>> projection, Expression<Func<T, bool>> whereClause, CancellationToken cancellationToken = default);

        Task<T> Add(T model, CancellationToken cancellationToken = default);

        Task<T> Delete(T model, CancellationToken cancellationToken = default);

        Task<T> Update(T model, CancellationToken cancellationToken = default);
    }
}