
using Datalayer.Context;
using Datalayer.Domain.Demographics;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.CQRS.Commands
{
    public class RemoveClientAddressCommand : ICustomCommand<ClientAddressKeys, IList<Address>>
    {
        private readonly IDbContextTransaction _transaction;
        public RemoveClientAddressCommand(IDbContextTransaction transaction = null)
        {
            this._transaction = transaction;
        }

        public async Task<IList<Address>> Execute(ASNDbContext context, ClientAddressKeys clientAddresss, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    var clientSelection = context.Clients
                                                 .Include(c => c.Addresses.Where(ph => clientAddresss.AddressIds.Contains(ph.Id)))
                                                 .Where(c => c.Id == clientAddresss.ClientId);

                    var Addresss = await context.Addresses
                                              .Include(p => p.Clients.Where(c => c.Id == clientAddresss.ClientId))
                                              .Where(p => clientAddresss.AddressIds.Contains(p.Id)).ToListAsync();

                    foreach (var client in clientSelection)
                    {
                        foreach (var Address in Addresss)
                        {
                            client.Addresses.Remove(Address);
                            Address.Clients.Remove(client);

                            client.ModifiedBy = clientAddresss.ModifiedBy;
                            client.ModifiedDate = clientAddresss.ModifiedDate;
                            client.State = Datalayer.Domain.ModelState.Updated;
                        }
                    }

                    await context.SaveChangesAsync(cancellationToken);
                }

                return await context.Clients.Include(c => c.Addresses).SelectMany(c => c.Addresses).ToListAsync();
            }
            catch (System.Exception)
            {
                await _transaction?.RollbackAsync();
                throw;
            }
        }

    }
}
