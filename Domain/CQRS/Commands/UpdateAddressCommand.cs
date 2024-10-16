using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Datalayer.Context;
using Datalayer.Domain;
using Datalayer.Domain.Demographics;
using Infrastructure.CQRS.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Commands
{
    public class UpdateAddressCommand : ICustomCommand<Address, Address>
    {
        private readonly IDbContextTransaction _transaction;
        public UpdateAddressCommand(IDbContextTransaction transaction = null)
        {
            _transaction = transaction;
        }

        public async Task<Address> Execute(ASNDbContext context, Address model, CancellationToken cancellationToken = default)
        {
            try
            {
                var selectedClients = model.Clients?.Select(c => c.Id) ?? new List<Guid>();
                var selectedOrganizations = model.Organizations?.Select(o => o.Id) ?? new List<Guid>();

                var Address = await context.Addresses
                                .Include(c => c.Clients)
                                .Include(o => o.Organizations)
                                .Where(p => p.Id == model.Id)
                                .FirstAsync(cancellationToken);

                if (!cancellationToken.IsCancellationRequested)
                {
                    Address.ModifiedBy = model.ModifiedBy;
                    Address.ModifiedDate = model.ModifiedDate;
                    Address.AddressType = model.AddressType;
                    Address.AddressLine1 = model.AddressLine1;
                    Address.AddressLine2 = model.AddressLine2;
                    Address.City = model.City;
                    Address.CountryCode = model.CountryCode;
                    Address.StateCode = model.StateCode;
                    Address.PostalCode = model.PostalCode;
                    Address.State = ModelState.Updated;

                    await context.Organizations
                        .Where(o => selectedOrganizations.Contains(o.Id) && !o.Addresses.Contains(Address))
                        .ForEachAsync(o =>
                        {
                            o.Addresses.Add(Address);
                            Address.Organizations.Add(o);
                        });

                    await context.Clients
                                 .Where(c => selectedClients.Contains(c.Id) && !c.Addresses.Contains(Address))
                                 .ForEachAsync(c =>
                                 {
                                     c.Addresses.Add(Address);
                                     Address.Clients.Add(c);
                                 });

                    await context.SaveChangesAsync(cancellationToken);
                }

                return Address;
            }
            catch (Exception)
            {
                await _transaction?.RollbackAsync();
                throw;
            }
        }
    }
}
