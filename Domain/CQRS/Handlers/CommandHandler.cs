using Datalayer.Context;
using Infrastructure.CQRS.Contracts;
using Infrastructure.Common.Events;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.CQRS.Handlers
{
    public class CommandHandler : ICommandHandler
    {
        private readonly ASNDbContext _context;
        private readonly IEventBus _eventBus;

        public CommandHandler(ASNDbContext context, IEventBus eventBus = null)
        {
            _context = context;
            _eventBus = eventBus;
        }

        public Task<IDbContextTransaction> CreateTransaction(CancellationToken token)
        {
            return _context.Database.BeginTransactionAsync(token);
        }

        public async Task<TOut> ExecuteCommand<T, TOut>(ICustomCommand<T, TOut> customCommand, T model, CancellationToken token) where T : class
        {
            token.ThrowIfCancellationRequested();
            return await customCommand.Execute(_context, model, token);
        }
    }
}
