using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Queries
{
    public static class GenericSelectionQuery
    {
        public static async Task<TOutput> GetEntityAsync<TEntity, TOutput>(
            this DbContext context,
            Expression<Func<TEntity, TOutput>> projection,
            Expression<Func<TEntity, bool>> whereExpression,
            CancellationToken cancellationToken = default)
            where TEntity : class
        {
            // Validate inputs
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (projection == null)
                throw new ArgumentNullException(nameof(projection));

            if (whereExpression == null)
                throw new ArgumentNullException(nameof(whereExpression));

            // Query the DbSet
            var query = context.Set<TEntity>()
                               .Where(whereExpression)
                               .Select(projection);

            // Return the first matching result or default
            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
