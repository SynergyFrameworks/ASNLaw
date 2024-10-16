using Microsoft.EntityFrameworkCore;
using Datalayer.Contracts;
using Infrastructure.Common.Paging;
using Infrastructure.Common.Sorting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Contracts
{
    public interface IQueryHandler<TDbContext> : IDisposable where TDbContext : DbContext
    {
        TDbContext Context { set; }

        Task<T> SelectHandler<T>(Expression<Func<T, T>> projection, Expression<Func<T, bool>> whereExpression, CancellationToken cancellationToken) where T : class, IEntity;

        Task<T> SelectHandler<T>(Expression<Func<T, T>> projection, Expression<Func<T, bool>> whereExpression) where T : class, IEntity;

        Task<int> CountHandler<T>(Expression<Func<T, bool>> whereExpression, CancellationToken cancellationToken) where T : class, IEntity;
        
        Task<ICollection<T>> SelectHandler<T>(Expression<Func<T, T>> projection, CancellationToken cancellationToken) where T : class, IEntity;
        
        Task<ICollection<TOutput>> SelectHandler<TModel, TOutput>(Expression<Func<TModel, TOutput>> projection, Expression<Func<TModel, bool>> whereExpression, IPaging pagingInfo, CancellationToken cancellationToken) where TModel : class, IEntity;

        Task<ICollection<TOutput>> SelectSortHandler<TModel, TOutput>(Expression<Func<TModel, TOutput>> projection, Expression<Func<TModel, bool>> whereExpression, IList<ISortingOption> sortingOptions, IPaging pagingInfo, CancellationToken cancellationToken) where TModel : class, IEntity;
    }
}
