using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Datalayer.Context;
using Datalayer.Domain.Demographics;
using Datalayer.Domain.Security;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models;
using Infrastructure.CQRS.Models.Phone;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Commands
{
    public class RemoveGroupUserCommand : ICustomCommand<GroupUserKeys, IList<User>>
    {
        private readonly IDbContextTransaction _transaction;
        public RemoveGroupUserCommand(IDbContextTransaction transaction = null)
        {
            this._transaction = transaction;
        }
        public async Task<IList<User>> Execute(ASNDbContext context, GroupUserKeys groupUsers, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    var groupSelection = context.Groups
                                                       .Include(c => c.Users.Where(ph => groupUsers.UserIds.Contains(ph.Id)))
                                                       .Where(c => c.Id == groupUsers.GroupId);

                    var users = await context.Users
                                        .Include(p => p.Groups.Where(c => c.Id == groupUsers.GroupId))
                                        .Where(p => groupUsers.UserIds.Contains(p.Id)).ToListAsync();

                    foreach (var group in groupSelection)
                    {
                        foreach (var user in users)
                        {
                            group.Users.Remove(user);
                            user.Groups.Remove(group);

                            group.ModifiedBy = groupUsers.ModifiedBy;
                            group.ModifiedDate = groupUsers.ModifiedDate;
                            group.State = Datalayer.Domain.ModelState.Deleted;
                        }
                    }

                    await context.SaveChangesAsync(cancellationToken);
                }

                return await context.Groups.Include(c => c.Users).SelectMany(c => c.Users).ToListAsync();
            }
            catch (System.Exception)
            {
                await _transaction?.RollbackAsync();
                throw;
            }
        }
    }
}
