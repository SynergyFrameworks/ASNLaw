using Infrastructure.Common.Paging;
using Infrastructure.Common.Sorting;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Queries
{
    public static class GenericSelectionsQuery
    {
        public static async Task<ICollection<TOutput>> GetEntitiesAsync<TEntity, TOutput>(
            this DbContext context,
            Expression<Func<TEntity, TOutput>> projection,
            Expression<Func<TEntity, bool>> whereExpression = null,
            IList<ISortingOption> sortOptions = null,
            IPaging pageInfo = null,
            CancellationToken cancellationToken = default)
            where TEntity : class
        {
            var dbSet = context.Set<TEntity>();
            var query = dbSet.AsQueryable();

            if (whereExpression != null)
            {
                query = query.Where(whereExpression);
            }

            // Apply sorting
            query = SortVisitor.ApplySorting(query, sortOptions);

            // Apply paging
            query = PagingVisitor.ApplyPaging(query, pageInfo);

            return await query
                         .Select(projection)
                         .ToListAsync(cancellationToken);
        }
    }

    public static class SortVisitor
    {
        public static IQueryable<TEntity> ApplySorting<TEntity>(IQueryable<TEntity> query, IList<ISortingOption> sortOptions)
        {
            if (sortOptions?.Count > 0)
            {
                foreach (var sortOption in sortOptions)
                {
                    query = OrderBy(query, sortOption);
                }
            }

            return query;
        }

        private static IQueryable<TEntity> OrderBy<TEntity>(IQueryable<TEntity> source, ISortingOption option)
        {
            ParameterExpression parameter = Expression.Parameter(source.ElementType, "x");
            MemberExpression property = Expression.Property(parameter, option.ColumnName);
            LambdaExpression lambdaExpression = Expression.Lambda(property, parameter);

            string methodName = option.IsColumnOrderDesc ? "OrderByDescending" : "OrderBy";
            Type[] types = { source.ElementType, property.Type };

            var methodCallExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                types,
                source.Expression,
                Expression.Quote(lambdaExpression)
            );

            return source.Provider.CreateQuery<TEntity>(methodCallExpression);
        }
    }

    public static class PagingVisitor
    {
        public static IQueryable<TEntity> ApplyPaging<TEntity>(IQueryable<TEntity> query, IPaging pageInfo)
        {
            if (pageInfo != null)
            {
                query = query.Skip((pageInfo.CurrentPage - 1) * pageInfo.PageCount)
                             .Take(pageInfo.PageCount);
            }

            return query;
        }
    }
}
