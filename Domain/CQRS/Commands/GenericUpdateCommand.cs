using Microsoft.EntityFrameworkCore;
using Datalayer.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Commands
{
    public static class GenericUpdateCommand
    {
        public static async Task<T> UpdateEntityAsync<T>(this DbContext context, T model, CancellationToken token = default) where T : class, IEntity, IAuditable
        {
            var foundEntity = await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == model.Id, token);

            if (foundEntity == null) throw new NullReferenceException("Entity was not found.");

            if (!token.IsCancellationRequested)
            {
                context.Update(model);

                var entry = context.Entry(model);
                entry.Property(e => e.CreatedBy).IsModified = false;
                entry.Property(e => e.CreatedDate).IsModified = false;
            }

            return foundEntity;
        }
    }
}
