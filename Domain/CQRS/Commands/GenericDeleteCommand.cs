using Microsoft.EntityFrameworkCore;
using Datalayer.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Commands
{
    public static class GenericDeleteCommand
    {
        public static async Task<T> DeleteEntityAsync<T>(this DbContext context, T model, CancellationToken token = default) where T : class, IEntity
        {
            var deleteEntity = await context.Set<T>().FindAsync(new object[] { model.Id });
            if (deleteEntity == null) throw new NullReferenceException("Entity was not found.");

            if (!token.IsCancellationRequested)
            {
                context.Remove(deleteEntity);
                return deleteEntity;
            }

            return null;
        }
    }
}
