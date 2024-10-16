
using Datalayer.Context;
using Datalayer.Contracts;
using Infrastructure.CQRS.Commands;
using Infrastructure.CQRS.Contracts;
using Infrastructure.Common.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Handlers
{
    //TODO: Pass type of T
    public class AddEvent : IEvent
    {
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public Guid Id { get; set; }
    }

    public class CrudHandler : ICrudHandler<ASNDbContext>
    {
        private ASNDbContext _context;
        private readonly IEventBus _eventBus;

        public CrudHandler(ASNDbContext context, IEventBus eventBus)
        {
            _context = context;
            _eventBus = eventBus;
        }

        public async Task<T> AddHandler<T>(T model, CancellationToken cancellationToken = default) where T : class, IEntity, IAuditable
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                await _context.InsertEntityAsync(model, cancellationToken);

                if (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        await _context.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync(cancellationToken);
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }

                    model.State = Datalayer.Domain.ModelState.Added;
                    //TODO: Pass the version...
                    _eventBus?.Commit(new AddEvent { Id = model.Id, TimeStamp = model.CreatedDate, Version = 1 });
                }
                else
                {
                    transaction.Rollback();
                }

                _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                return model;
            }
        }

        public async Task<T> DeleteHandler<T>(T model, CancellationToken cancellationToken) where T : class, IEntity, IAuditable
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                await _context.DeleteEntityAsync(model, cancellationToken);
                if (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        await _context.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync(cancellationToken);
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }

                    model.State = Datalayer.Domain.ModelState.Deleted;
                }
                else
                {
                    transaction.Rollback();
                }

                _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                return model;
            }
        }

        public async Task<T> UpdateHandler<T>(T model, CancellationToken cancellationToken) where T : class, IEntity, IAuditable
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                await _context.UpdateEntityAsync(model, cancellationToken);

                if (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        await _context.SaveChangesAsync(cancellationToken);
                        await transaction.CommitAsync(cancellationToken);
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }

                    model.State = Datalayer.Domain.ModelState.Updated;
                }
                else
                {
                    transaction.Rollback();
                }

                _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                return model;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
