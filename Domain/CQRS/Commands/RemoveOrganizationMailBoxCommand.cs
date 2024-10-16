using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Datalayer.Context;
using Datalayer.Domain.Demographics;
using Datalayer.Domain;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models.MailBox;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Commands
{
    public class RemoveOrganizationMailBoxCommand : ICustomCommand<OrganizationMailBoxKeys, IList<MailBox>>
    {
        private readonly IDbContextTransaction _transaction;
        public RemoveOrganizationMailBoxCommand(IDbContextTransaction transaction = null)
        {
            this._transaction = transaction;
        }
        public async Task<IList<MailBox>> Execute(ASNDbContext context, OrganizationMailBoxKeys organizationMailBoxs, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    var organizationSelection = context.Organizations
                                                       .Include(c => c.MailBoxes.Where(ph => organizationMailBoxs.MailBoxIds.Contains(ph.Id)))
                                                       .Where(c => c.Id == organizationMailBoxs.OrganizationId);

                    var MailBoxs = await context.MailBoxes
                                        .Include(p => p.Clients.Where(c => c.Id == organizationMailBoxs.OrganizationId))
                                        .Where(p => organizationMailBoxs.MailBoxIds.Contains(p.Id)).ToListAsync();

                    foreach (var organization in organizationSelection)
                    {
                        foreach (var MailBox in MailBoxs)
                        {
                            organization.MailBoxes.Remove(MailBox);
                            MailBox.Organizations.Remove(organization);

                            organization.ModifiedBy = organizationMailBoxs.ModifiedBy;
                            organization.ModifiedDate = organizationMailBoxs.ModifiedDate;
                            organization.State = Datalayer.Domain.ModelState.Deleted;
                        }
                    }

                    await context.SaveChangesAsync(cancellationToken);
                }

                return await context.Organizations.Include(c => c.MailBoxes).SelectMany(c => c.MailBoxes).ToListAsync();
            }
            catch (System.Exception)
            {
                await _transaction?.RollbackAsync();
                throw;
            }
        }
    }
}
