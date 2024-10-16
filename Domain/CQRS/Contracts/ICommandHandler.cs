using Microsoft.EntityFrameworkCore.Storage;
using Datalayer.Context;
using Datalayer.Contracts;
using Infrastructure.CQRS.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Contracts
{
    public interface ICommandHandler
    {
        Task<IDbContextTransaction> CreateTransaction(CancellationToken token);

        Task<TOut> ExecuteCommand<T, TOut>(ICustomCommand<T, TOut> customCommand, T model, CancellationToken token) where T : class;
    }
}