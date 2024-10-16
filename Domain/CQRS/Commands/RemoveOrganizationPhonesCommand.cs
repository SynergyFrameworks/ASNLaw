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
    public class RemoveOrganizationPhonesCommand : ICustomCommand<OrganizationPhoneKeys, IList<Phone>>
    {
        private readonly IDbContextTransaction _transaction;
        public RemoveOrganizationPhonesCommand(IDbContextTransaction transaction = null)
        {
            this._transaction = transaction;
        }
        public async Task<IList<Phone>> Execute(ASNDbContext context, OrganizationPhoneKeys organizationPhones, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    var organizationSelection = context.Organizations
                                                       .Include(c => c.Phones.Where(ph => organizationPhones.PhoneIds.Contains(ph.Id)))
                                                       .Where(c => c.Id == organizationPhones.OrganizationId);

                    var phones = await context.Phones
                                        .Include(p => p.Clients.Where(c => c.Id == organizationPhones.OrganizationId))
                                        .Where(p => organizationPhones.PhoneIds.Contains(p.Id)).ToListAsync();

                    foreach (var organization in organizationSelection)
                    {
                        foreach (var phone in phones)
                        {
                            organization.Phones.Remove(phone);
                            phone.Organizations.Remove(organization);

                            organization.ModifiedBy = organizationPhones.ModifiedBy;
                            organization.ModifiedDate = organizationPhones.ModifiedDate;
                            organization.State = Datalayer.Domain.ModelState.Deleted;
                        }
                    }

                    await context.SaveChangesAsync(cancellationToken);
                }

                return await context.Organizations.Include(c => c.Phones).SelectMany(c => c.Phones).ToListAsync();
            }
            catch (System.Exception)
            {
                await _transaction?.RollbackAsync();
                throw;
            }
        }
    }
}
