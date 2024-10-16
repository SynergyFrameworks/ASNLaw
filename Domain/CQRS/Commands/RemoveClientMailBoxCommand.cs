
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Datalayer.Context;
using Datalayer.Domain;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models.MailBox;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Commands
{
    public class RemoveClientMailBoxCommand : ICustomCommand<ClientMailBoxKeys, IList<MailBox>>
    {
        private readonly IDbContextTransaction _transaction;
        public RemoveClientMailBoxCommand(IDbContextTransaction transaction = null)
        {
            _transaction = transaction;
        }

        public async Task<IList<MailBox>> Execute(ASNDbContext context, ClientMailBoxKeys clientMailBoxs, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    IQueryable<ASNClient> clientSelection = context.Clients
                                                 .Include(c => c.MailBoxes.Where(ph => clientMailBoxs.MailBoxIds.Contains(ph.Id)))
                                                 .Where(c => c.Id == clientMailBoxs.ClientId);

                    List<MailBox> MailBoxs = await context.MailBoxes
                                              .Include(p => p.Clients.Where(c => c.Id == clientMailBoxs.ClientId))
                                              .Where(p => clientMailBoxs.MailBoxIds.Contains(p.Id)).ToListAsync();

                    foreach (ASNClient client in clientSelection)
                    {
                        foreach (MailBox MailBox in MailBoxs)
                        {
                            client.MailBoxes.Remove(MailBox);
                            MailBox.Clients.Remove(client);

                            client.ModifiedBy = clientMailBoxs.ModifiedBy;
                            client.ModifiedDate = clientMailBoxs.ModifiedDate;
                            client.State = Datalayer.Domain.ModelState.Updated;
                        }
                    }

                    await context.SaveChangesAsync(cancellationToken);
                }

                return await context.Clients.Include(c => c.MailBoxes).SelectMany(c => c.MailBoxes).ToListAsync();
            }
            catch (System.Exception)
            {
                await _transaction?.RollbackAsync();
                throw;
            }
        }
    }
}
