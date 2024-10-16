using Datalayer.Context;
using Datalayer.Contracts;
using Infrastructure.CQRS.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.Services
{
    public abstract class CrudServiceBase<T> where T : class, IEntity, IAuditable
    {
        internal protected readonly ICrudHandler<ASNDbContext> _handler;
        internal protected readonly IQueryHandler<ASNDbContext> _queryHandler;

        public CrudServiceBase(ICrudHandler<ASNDbContext> handler, IQueryHandler<ASNDbContext> queryHandler)
        {
            _handler = handler;
            _queryHandler = queryHandler;
        }

        public virtual async Task<T> Add(T model, CancellationToken cancellationToken = default)
        {
            return await _handler.AddHandler(model, cancellationToken);
        }

        public virtual async Task<T> Delete(T model, CancellationToken cancellationToken = default)
        {
            return await _handler.DeleteHandler(model, cancellationToken);
        }

        public virtual async Task<IList<T>> Get(Expression<Func<T, T>> projection, CancellationToken cancellationToken = default)
        {
            return (await _queryHandler.SelectHandler(projection, cancellationToken)).ToList();
        }

        public virtual async Task<T> Get(Expression<Func<T, T>> projection, Guid id, CancellationToken cancellationToken = default)
        {
            return await _queryHandler.SelectHandler(projection, c => c.Id == id, cancellationToken);
        }

        public virtual async Task<T> Update(T model, CancellationToken cancellationToken = default)
        {
            return await _handler.UpdateHandler(model, cancellationToken);
        }
    }
}
