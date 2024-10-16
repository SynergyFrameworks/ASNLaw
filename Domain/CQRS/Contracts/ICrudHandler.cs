using Microsoft.EntityFrameworkCore;
using Datalayer.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace Infrastructure.CQRS.Contracts
{
    public interface ICrudHandler<TDbContext> : IDisposable where TDbContext : DbContext
    {
        Task<T> AddHandler<T>(T model, CancellationToken cancellationToken = default) where T : class, IEntity, IAuditable;
        
        Task<T> DeleteHandler<T>(T model, CancellationToken cancellationToken = default) where T : class, IEntity, IAuditable;
        
        Task<T> UpdateHandler<T>(T model, CancellationToken cancellationToken = default) where T : class, IEntity, IAuditable;
    }
}
