using Datalayer.Context;
using Datalayer.Contracts;
using Infrastructure.CQRS.Contracts;
using Infrastructure.Common.Sorting;
using System.Linq.Expressions;
using Infrastructure.CQRS.Queries;
using Infrastructure.Common.Paging;

namespace Infrastructure.CQRS.Handlers
{
    public class QueryHandler : IQueryHandler<ASNDbContext>
    {
        private ASNDbContext _context;

        public QueryHandler(ASNDbContext context)
        {
            _context = context;
        }

        public ASNDbContext Context { set => _context = value; }

        public async Task<ICollection<T>> SelectHandler<T>(Expression<Func<T, T>> projection, CancellationToken cancellationToken) where T : class, IEntity
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _context.GetEntitiesAsync(projection, cancellationToken: cancellationToken);
        }

        public async Task<T> SelectHandler<T>(Expression<Func<T, T>> projection, Expression<Func<T, bool>> whereExpression, CancellationToken cancellationToken) where T : class, IEntity
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _context.GetEntityAsync(projection, whereExpression, cancellationToken);
        }

        public async Task<T> SelectHandler<T>(Expression<Func<T, T>> projection, Expression<Func<T, bool>> whereExpression) where T : class, IEntity
        {
            return await _context.GetEntityAsync(projection, whereExpression);
        }

        public async Task<ICollection<TOutput>> SelectHandler<TModel, TOutput>(Expression<Func<TModel, TOutput>> projection, Expression<Func<TModel, bool>> whereExpression, IPaging pagingInfo, CancellationToken cancellationToken) where TModel : class, IEntity
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _context.GetEntitiesAsync(projection, whereExpression, pageInfo: pagingInfo, cancellationToken: cancellationToken);
        }

        public async Task<ICollection<TOutput>> SelectSortHandler<TModel, TOutput>(Expression<Func<TModel, TOutput>> projection, Expression<Func<TModel, bool>> whereExpression, IList<ISortingOption> sortingOptions, IPaging pagingInfo, CancellationToken cancellationToken) where TModel : class, IEntity
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _context.GetEntitiesAsync(projection, whereExpression, sortingOptions, pagingInfo, cancellationToken);
        }

        public async Task<int> CountHandler<T>(Expression<Func<T, bool>> whereExpression, CancellationToken cancellationToken) where T : class, IEntity
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _context.GetCountAsync(whereExpression: whereExpression, cancellationToken);
        }
        public void Dispose()
        {
            _context.Dispose();
        }

     
    }
}
