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
    public class UpdateMailBoxCommand : ICustomCommand<MailBox, MailBox>
    {
        private readonly IDbContextTransaction _transaction;
        public UpdateMailBoxCommand(IDbContextTransaction transaction = null)
        {
            _transaction = transaction;
        }

        public async Task<MailBox> Execute(ASNDbContext context, MailBox model, CancellationToken cancellationToken = default)
        {
            try
            {
                var selectedClients = model.Clients?.Select(c => c.Id) ?? new List<Guid>();
                var selectedOrganizations = model.Organizations?.Select(o => o.Id) ?? new List<Guid>();

                var MailBox = await context.MailBoxes
                                .Include(c => c.Clients)
                                .Include(o => o.Organizations)
                                .Where(p => p.Id == model.Id)
                                .FirstAsync(cancellationToken);

                if (!cancellationToken.IsCancellationRequested)
                {
                    MailBox.ModifiedBy = model.ModifiedBy;
                    MailBox.ModifiedDate = model.ModifiedDate;
                    MailBox.FromAddress = model.FromAddress;
                    MailBox.ServerPassword = model.ServerPassword;
                    MailBox.ServerUserName = model.ServerUserName;
                    MailBox.AdminEmail = model.AdminEmail;
              
                    MailBox.State = ModelState.Updated;

                    await context.Organizations
                        .Where(o => selectedOrganizations.Contains(o.Id) && !o.MailBoxes.Contains(MailBox))
                        .ForEachAsync(o =>
                        {
                            o.MailBoxes.Add(MailBox);
                            MailBox.Organizations.Add(o);
                        });

                    await context.Clients
                                 .Where(c => selectedClients.Contains(c.Id) && !c.MailBoxes.Contains(MailBox))
                                 .ForEachAsync(c =>
                                 {
                                     c.MailBoxes.Add(MailBox);
                                     MailBox.Clients.Add(c);
                                 });

                    await context.SaveChangesAsync(cancellationToken);
                }

                return MailBox;
            }
            catch (Exception)
            {
                await _transaction?.RollbackAsync();
                throw;
            }
        }
    }
}
