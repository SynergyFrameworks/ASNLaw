using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Datalayer.Context;
using Datalayer.Domain.Demographics;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models.Phone;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Commands
{
    public class RemoveClientPhonesCommand : ICustomCommand<ClientPhoneKeys, IList<Phone>>
    {
        private readonly IDbContextTransaction _transaction;
        public RemoveClientPhonesCommand(IDbContextTransaction transaction = null)
        {
            this._transaction = transaction;
        }

        public async Task<IList<Phone>> Execute(ASNDbContext context, ClientPhoneKeys clientPhones, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    var clientSelection = context.Clients
                                                 .Include(c => c.Phones.Where(ph => clientPhones.PhoneIds.Contains(ph.Id)))
                                                 .Where(c => c.Id == clientPhones.ClientId);

                    var phones = await context.Phones
                                              .Include(p => p.Clients.Where(c => c.Id == clientPhones.ClientId))
                                              .Where(p => clientPhones.PhoneIds.Contains(p.Id)).ToListAsync();

                    foreach (var client in clientSelection)
                    {
                        foreach (var phone in phones)
                        {
                            client.Phones.Remove(phone);
                            phone.Clients.Remove(client);

                            client.ModifiedBy = clientPhones.ModifiedBy;
                            client.ModifiedDate = clientPhones.ModifiedDate;
                            client.State = Datalayer.Domain.ModelState.Updated;
                        }
                    }

                    await context.SaveChangesAsync(cancellationToken);
                }

                return await context.Clients.Include(c => c.Phones).SelectMany(c => c.Phones).ToListAsync();
            }
            catch (System.Exception)
            {
                await _transaction?.RollbackAsync();
                throw;
            }
        }
    }
}
