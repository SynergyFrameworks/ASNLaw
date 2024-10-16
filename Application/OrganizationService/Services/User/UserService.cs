
using LinqKit;
using OrganizationService.Model;
using Datalayer.Context;
using Datalayer.Domain;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models;
using Infrastructure.CQRS.Commands;
using Infrastructure.Common.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Datalayer.Domain.Security;
using Datalayer.Domain.Group;
using Infrastructure.CQRS.Projections;
using Infrastructure.CQRS.Projections.User;

namespace Organization.Services
{
    public class ASNUserService : CrudServiceBase<User>, IService<User, DefaultSearch<UserSearchResult>>
    {
        private readonly IService<ASNGroup, DefaultSearch<GroupSearchResult>> _groupService;
        private readonly IService<ASNClient, DefaultSearch<ClientSearchResult>> _clientService;
        private readonly ICommandHandler _commandHandler;

        public ASNUserService(ICrudHandler<ASNDbContext> handler
            , IQueryHandler<ASNDbContext> queryHandler
            , IService<ASNGroup, DefaultSearch<GroupSearchResult>> groupService
            , IService<ASNClient, DefaultSearch<ClientSearchResult>> clientService
            , ICommandHandler commandHandler

            ) : base(handler, queryHandler)
        {
            _groupService = groupService;
            _clientService = clientService;
            _commandHandler = commandHandler;
        }

        public override async Task<User> Add(User model, CancellationToken cancellationToken = default)
        {
            var groups = model.Groups?.ToArray() ?? new ASNGroup[] { };
            var clients = model.Clients?.ToArray() ?? new ASNClient[] { };

            model.Clients = new HashSet<ASNClient>();
            model.Groups = new HashSet<ASNGroup>();

            var user = await base.Add(model, cancellationToken);

            try
            {
                await UpdateGroups(groups, user, cancellationToken);
                await UpdateClients(clients, user, cancellationToken);
            }
            catch (Exception)
            {
                await base.Delete(user);
                throw;
            }

            user.Groups = user.Groups.Select(org => new ASNGroup { Id = org.Id, Name = org.Name }).ToArray();
            user.Clients = user.Clients.Select(client => new ASNClient { Id = client.Id, Name = client.Name, ClientNo = client.ClientNo }).ToArray();

            return user;
        }

        public override async Task<User> Update(User model, CancellationToken cancellationToken = default)
        {
            using (var transaction = await _commandHandler.CreateTransaction(cancellationToken))
            {
                var currentUser = await _commandHandler.ExecuteCommand(new UpdateUserCommand(transaction), model, cancellationToken);

                var currrentUserClients = currentUser.Clients.ToList();
                var currentUserGroups = currentUser.Groups.ToList();

                await RemoveClientUsers(model, currrentUserClients, transaction, cancellationToken);
                await RemoveGroupUsers(model, currentUserGroups, transaction, cancellationToken);

                currentUser.Groups = currentUser.Groups.Select(org => new ASNGroup { Id = org.Id, Name = org.Name }).ToArray();
                currentUser.Clients = currentUser.Clients.Select(client => new ASNClient { Id = client.Id, Name = client.Name, ClientNo = client.ClientNo }).ToArray();

                await transaction.CommitAsync(cancellationToken);

                return currentUser;
            }
        }

        public async Task<DefaultSearch<UserSearchResult>> Query(DefaultSearch<UserSearchResult> search, CancellationToken cancellationToken = default)
        {
            var whereClauseBuilder = PredicateBuilder.New<User>(x => true);

            if (!String.IsNullOrWhiteSpace(search.NameSearch))
            {
                whereClauseBuilder.And(user => user.UserName.Contains(search.NameSearch));
                whereClauseBuilder.Or(user => user.Name.Contains(search.NameSearch));
                whereClauseBuilder.Or(user => user.Groups.Count(o => o.Name.Contains(search.NameSearch)) > 0);
                whereClauseBuilder.Or(user => user.Clients.Count(o => o.Name.Contains(search.NameSearch)) > 0);
            }

            if (search.CreatedDateSearchRange != null)
            {
                whereClauseBuilder.And(row => row.CreatedDate.Date >= search.CreatedDateSearchRange.FromDate.Date);
                whereClauseBuilder.And(row => row.CreatedDate.Date <= search.CreatedDateSearchRange.ToDate.Date);
            }

            var results = await _queryHandler.SelectSortHandler(UserSearchDetails.UserSearch, whereClauseBuilder, search.SortOptions?.OfType<ISortingOption>().ToList(), search, cancellationToken);

            search.TotalOfRecords = await _queryHandler.CountHandler<User>(whereClauseBuilder, cancellationToken);

            search.Results = results.ToList();

            return search;
        }

        public async Task<User> Query(Expression<Func<User, User>> projection, Expression<Func<User, bool>> whereClause, CancellationToken cancellationToken = default)
        {
            return await _queryHandler.SelectHandler(projection, whereClause, cancellationToken);
        }

        private async Task RemoveGroupUsers(User model, List<ASNGroup> groupUsers, Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            var updateGroups = model.Groups?.Select(e => e.Id).ToList() ?? new List<Guid>();

            foreach (var groupUser in groupUsers.Where(c => !updateGroups.Contains(c.Id)))
            {
                await _commandHandler.ExecuteCommand(new RemoveGroupUserCommand(transaction),
                    new GroupUserKeys
                    {
                        GroupId = groupUser.Id,
                        UserIds = groupUser.Users.Select(p => p.Id).ToList(),
                        ModifiedBy = model.ModifiedBy,
                        ModifiedDate = model.ModifiedDate.Value
                    }, cancellationToken);
            }
        }

        private async Task RemoveClientUsers(User model, List<ASNClient> userClients, Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            var updateClients = model.Clients?.Select(e => e.Id).ToList() ?? new List<Guid>();

            foreach (var userClient in userClients.Where(c => !updateClients.Contains(c.Id)))
            {
                await _commandHandler.ExecuteCommand(new RemoveClientUserCommand(transaction),
                   new ClientUserKeys
                   {
                       ClientId = userClient.Id,
                       UserIds = userClient.Users.Select(p => p.Id).ToList(),
                       ModifiedBy = model.ModifiedBy,
                       ModifiedDate = model.ModifiedDate.Value
                   }, cancellationToken);
            }
        }

        private async Task UpdateClients(ASNClient[] clients, User user, CancellationToken cancellationToken)
        {
            foreach (var client in clients)
            {
                var current = await _clientService.Get(client => client, client.Id, cancellationToken);

                current.ModifiedBy = "system";
                current.ModifiedDate = DateTime.UtcNow;
                current.Users.Add(user);

                await _clientService.Update(current, cancellationToken);
            }
        }

        private async Task UpdateGroups(ASNGroup[] ASNGroups, User user, CancellationToken cancellationToken)
        {
            foreach (var ASNGroup in ASNGroups)
            {
                var current = await _groupService.Get(grp => grp, ASNGroup.Id, cancellationToken);

                current.ModifiedBy = "system";
                current.ModifiedDate = DateTime.UtcNow;
                current.Users.Add(user);

                await _groupService.Update(current, cancellationToken);
            }
        }
    }
}
