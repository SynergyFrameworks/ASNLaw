using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Datalayer.Context;
using Datalayer.Domain.Demographics;
using Datalayer.Domain.Security;
using Infrastructure.CQRS.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Commands
{
    public class UpdateUserCommand : ICustomCommand<User, User>
    {
        private readonly IDbContextTransaction _transaction;
        public UpdateUserCommand(IDbContextTransaction transaction = null)
        {
            _transaction = transaction;
        }

        public async Task<User> Execute(ASNDbContext context, User model, CancellationToken cancellationToken = default)
        {
            try
            {
                var selectedClients = model.Clients?.Select(c => c.Id) ?? new List<Guid>();
                var selectedGroups = model.Groups?.Select(o => o.Id) ?? new List<Guid>();

                var user = await context.Users
                                .Include(c => c.Clients)
                                .Include(o => o.Groups)
                                .Where(p => p.Id == model.Id)
                                .FirstAsync(cancellationToken);

                if (!cancellationToken.IsCancellationRequested)
                {
                    user.ModifiedBy = model.ModifiedBy;
                    user.ModifiedDate = model.ModifiedDate;
                    user.IdentityUserId = model.IdentityUserId;
                    user.ImageUrl = model.ImageUrl;
                    user.IsActive = model.IsActive;
                    user.Name = model.Name;
                    user.UserName = model.UserName;
                    user.State = Datalayer.Domain.ModelState.Updated;

                    await context.Groups
                        .Where(o => selectedGroups.Contains(o.Id) && !o.Users.Contains(user))
                        .ForEachAsync(o =>
                        {
                            o.Users.Add(user);
                            user.Groups.Add(o);
                        });

                    await context.Clients
                                 .Where(c => selectedClients.Contains(c.Id) && !c.Users.Contains(user))
                                 .ForEachAsync(c =>
                                 {
                                     c.Users.Add(user);
                                     user.Clients.Add(c);
                                 });

                    await context.SaveChangesAsync(cancellationToken);
                }

                return user;
            }
            catch (Exception)
            {
                await _transaction?.RollbackAsync();
                throw;
            }
        }
    }
}
