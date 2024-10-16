using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Datalayer.Context;
using Datalayer.Domain.Demographics;
using Infrastructure.CQRS.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Commands
{
    public class UpdatePhoneCommand : ICustomCommand<Phone, Phone>
    {
        private readonly IDbContextTransaction _transaction;
        public UpdatePhoneCommand(IDbContextTransaction transaction = null)
        {
            _transaction = transaction;
        }

        public async Task<Phone> Execute(ASNDbContext context, Phone model, CancellationToken cancellationToken = default)
        {
            try
            {
                var selectedClients = model.Clients?.Select(c => c.Id) ?? new List<Guid>();
                var selectedOrganizations = model.Organizations?.Select(o => o.Id) ?? new List<Guid>();
                
                var phone = await context.Phones
                                .Include(c => c.Clients)
                                .Include(o => o.Organizations)
                                .Where(p => p.Id == model.Id)
                                .FirstAsync(cancellationToken);

                if (!cancellationToken.IsCancellationRequested)
                {
                    phone.ModifiedBy = model.ModifiedBy;
                    phone.ModifiedDate = model.ModifiedDate;
                    phone.CountryPrefix = model.CountryPrefix;
                    phone.PhoneType = model.PhoneType;
                    phone.PhoneNumber = model.PhoneNumber;
                    phone.State = Datalayer.Domain.ModelState.Updated;

                    await context.Organizations
                        .Where(o => selectedOrganizations.Contains(o.Id) && !o.Phones.Contains(phone))
                        .ForEachAsync(o =>
                        {
                            o.Phones.Add(phone);
                            phone.Organizations.Add(o);
                        });

                    await context.Clients
                                 .Where(c => selectedClients.Contains(c.Id) && !c.Phones.Contains(phone))
                                 .ForEachAsync(c =>
                                 {
                                     c.Phones.Add(phone);
                                     phone.Clients.Add(c);
                                 });

                    await context.SaveChangesAsync(cancellationToken);
                }

                return phone;
            }
            catch (Exception)
            {
                await _transaction?.RollbackAsync();
                throw;
            }
        }
    }
}
