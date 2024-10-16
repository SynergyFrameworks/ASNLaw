using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Datalayer.Context;
using Datalayer.Domain.Demographics;
using Datalayer.Domain.Security;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models;
using Infrastructure.CQRS.Models.Phone;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Commands
{
    public class RemoveClientUserCommand : ICustomCommand<ClientUserKeys, IList<User>>
    {
        private readonly IDbContextTransaction _transaction;
        public RemoveClientUserCommand(IDbContextTransaction transaction = null)
        {
            this._transaction = transaction;
        }
        public async Task<IList<User>> Execute(ASNDbContext context, ClientUserKeys clientUsers, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    var clientSelection = context.Clients
                                                       .Include(c => c.Users.Where(ph => clientUsers.UserIds.Contains(ph.Id)))
                                                       .Where(c => c.Id == clientUsers.ClientId);

                    var users = await context.Users
                                        .Include(p => p.Clients.Where(c => c.Id == clientUsers.ClientId))
                                        .Where(p => clientUsers.UserIds.Contains(p.Id)).ToListAsync();

                    foreach (var client in clientSelection)
                    {
                        foreach (var user in users)
                        {
                            client.Users.Remove(user);
                            user.Clients.Remove(client);

                            client.ModifiedBy = clientUsers.ModifiedBy;
                            client.ModifiedDate = clientUsers.ModifiedDate;
                            client.State = Datalayer.Domain.ModelState.Deleted;
                        }
                    }

                    await context.SaveChangesAsync(cancellationToken);
                }

                return await context.Groups.Include(c => c.Users).SelectMany(c => c.Users).ToListAsync();
            }
            catch (System.Exception)
            {
                await _transaction?.RollbackAsync();
                throw;
            }
        }
    }
}
