using Datalayer.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CQRS.Commands
{
    public static class GenericInsertCommand
    {
        public static async Task<int> InsertEntityAsync<T>(this DbContext context, T newEntity, CancellationToken token = default) where T : class, IEntity
        {
            var result = await context.Set<T>().AddAsync(newEntity, token);
            return (int)result.State;
        }
    }
}
