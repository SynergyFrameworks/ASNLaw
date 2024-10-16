using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Datalayer.Context;
using Datalayer.Domain.Demographics;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models.Addresses;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Commands
{
    public class RemoveOrganizationAddressCommand : ICustomCommand<OrganizationAddressKeys, IList<Address>>
    {
        private readonly IDbContextTransaction _transaction;
        public RemoveOrganizationAddressCommand(IDbContextTransaction transaction = null)
        {
            this._transaction = transaction;
        }
        public async Task<IList<Address>> Execute(ASNDbContext context, OrganizationAddressKeys organizationAddresss, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    var organizationSelection = context.Organizations
                                                       .Include(c => c.Addresses.Where(ph => organizationAddresss.AddressIds.Contains(ph.Id)))
                                                       .Where(c => c.Id == organizationAddresss.OrganizationId);

                    var Addresss = await context.Addresses
                                        .Include(p => p.Clients.Where(c => c.Id == organizationAddresss.OrganizationId))
                                        .Where(p => organizationAddresss.AddressIds.Contains(p.Id)).ToListAsync();

                    foreach (var organization in organizationSelection)
                    {
                        foreach (var Address in Addresss)
                        {
                            organization.Addresses.Remove(Address);
                            Address.Organizations.Remove(organization);

                            organization.ModifiedBy = organizationAddresss.ModifiedBy;
                            organization.ModifiedDate = organizationAddresss.ModifiedDate;
                            organization.State = Datalayer.Domain.ModelState.Deleted;
                        }
                    }

                    await context.SaveChangesAsync(cancellationToken);
                }

                return await context.Organizations.Include(c => c.Addresses).SelectMany(c => c.Addresses).ToListAsync();
            }
            catch (System.Exception)
            {
                await _transaction?.RollbackAsync();
                throw;
            }
        }
    }
}
