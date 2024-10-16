
using LinqKit;
using OrganizationService.Model;
using Datalayer.Context;
using Datalayer.Domain;
using Infrastructure.CQRS.Commands;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Handlers;
using Infrastructure.CQRS.Models;
using Infrastructure.CQRS.Models.MailBox;
using Infrastructure.CQRS.Projections;
using Infrastructure.Common.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;



namespace Organization.Services
{

    public class MailBoxService : CrudServiceBase<MailBox>, IService<MailBox, DefaultSearch<MailBoxSearchResult>>
    {
        private readonly IService<Datalayer.Domain.Organization, DefaultSearch<OrganizationSearchResult>> _organizationService;
        private readonly IService<ASNClient, DefaultSearch<ClientSearchResult>> _clientService;
        private readonly ICommandHandler _commandHandler;
        private CommandHandler commandHandler;

        public MailBoxService(ICrudHandler<ASNDbContext> handler
            , IQueryHandler<ASNDbContext> queryHandler
            , IService<Datalayer.Domain.Organization, DefaultSearch<OrganizationSearchResult>> organizationService
            , IService<ASNClient, DefaultSearch<ClientSearchResult>> clientService
            , ICommandHandler commandHandler

            ) : base(handler, queryHandler)
        {
            _commandHandler = commandHandler;
            _organizationService = organizationService;
            _clientService = clientService;

        }

        public MailBoxService(ICrudHandler<ASNDbContext> handler, IQueryHandler<ASNDbContext> queryHandler, CommandHandler commandHandler) : base(handler, queryHandler)
        {
            this.commandHandler = commandHandler;
        }

        public override async Task<MailBox> Add(MailBox model, CancellationToken cancellationToken = default)
        {
            var organizations = model.Organizations?.ToArray() ?? new Datalayer.Domain.Organization[] { };
            var clients = model.Clients?.ToArray() ?? new ASNClient[] { };

            model.Clients = new HashSet<ASNClient>();
            model.Organizations = new HashSet<Datalayer.Domain.Organization>();

            var MailBox = await base.Add(model, cancellationToken);

            try
            {
                await UpdateOrganizations(organizations, MailBox, cancellationToken);
                await UpdateClients(clients, MailBox, cancellationToken);
            }
            catch (Exception)
            {
                await base.Delete(MailBox);
                throw;
            }

            MailBox.Organizations = MailBox.Organizations.Select(org => new Datalayer.Domain.Organization { Id = org.Id, Name = org.Name }).ToArray();
            MailBox.Clients = MailBox.Clients.Select(client => new ASNClient { Id = client.Id, Name = client.Name, ClientNo = client.ClientNo }).ToArray();

            return MailBox;
        }

        public override async Task<MailBox> Update(MailBox model, CancellationToken cancellationToken = default)
        {
            using (var transaction = await _commandHandler.CreateTransaction(cancellationToken))
            {
                var currentMailBox = await _commandHandler.ExecuteCommand(new UpdateMailBoxCommand(transaction), model, cancellationToken);

                var currrentMailBoxClients = currentMailBox.Clients.ToList();
                var currentMailBoxOrganizations = currentMailBox.Organizations.ToList();

                await RemoveClientMailBoxs(model, currrentMailBoxClients, transaction, cancellationToken);
                await RemoveOrganizationMailBoxs(model, currentMailBoxOrganizations, transaction, cancellationToken);

                currentMailBox.Organizations = currentMailBox.Organizations.Select(org => new Datalayer.Domain.Organization { Id = org.Id, Name = org.Name }).ToArray();
                currentMailBox.Clients = currentMailBox.Clients.Select(client => new ASNClient { Id = client.Id, Name = client.Name, ClientNo = client.ClientNo }).ToArray();

                await transaction.CommitAsync(cancellationToken);

                return currentMailBox;
            }
        }

        public async Task<DefaultSearch<MailBoxSearchResult>> Query(DefaultSearch<MailBoxSearchResult> search, CancellationToken cancellationToken = default)
        {
            var whereClauseBuilder = PredicateBuilder.New<MailBox>(x => true);

            if (!String.IsNullOrWhiteSpace(search.NameSearch))
            {
                whereClauseBuilder.And(MailBox => search.NameSearch.Contains(MailBox.Server));
                whereClauseBuilder.Or(MailBox => search.NameSearch.Contains(MailBox.AdminEmail));
                whereClauseBuilder.Or(MailBox => search.NameSearch.Contains(MailBox.ServerUserName));
                whereClauseBuilder.Or(MailBox => search.NameSearch.Contains(MailBox.FromAddress));
                whereClauseBuilder.Or(MailBox => MailBox.Organizations.Count(o => o.Name.Contains(search.NameSearch)) > 0);
                whereClauseBuilder.Or(MailBox => MailBox.Clients.Count(o => o.Name.Contains(search.NameSearch)) > 0);
            }

            if (search.CreatedDateSearchRange != null)
            {
                whereClauseBuilder.And(row => row.CreatedDate.Date >= search.CreatedDateSearchRange.FromDate.Date);
                whereClauseBuilder.And(row => row.CreatedDate.Date <= search.CreatedDateSearchRange.ToDate.Date);
            }

            var results = await _queryHandler.SelectSortHandler(MailBoxSearchDetails.MailBoxSearch, whereClauseBuilder, search.SortOptions?.OfType<ISortingOption>().ToList(), search, cancellationToken);

            search.TotalOfRecords = await _queryHandler.CountHandler<MailBox>(whereClauseBuilder, cancellationToken);

            search.Results = results.ToList();

            return search;
        }

        public async Task<MailBox> Query(Expression<Func<MailBox, MailBox>> projection, Expression<Func<MailBox, bool>> whereClause, CancellationToken cancellationToken = default)
        {
            return await _queryHandler.SelectHandler(projection, whereClause, cancellationToken);
        }

        private async Task RemoveOrganizationMailBoxs(MailBox model, List<Datalayer.Domain.Organization> MailBoxOrganizations, Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            var updateOrganizanitions = model.Organizations?.Select(e => e.Id).ToList() ?? new List<Guid>();

            foreach (var organizationMailBox in MailBoxOrganizations.Where(c => !updateOrganizanitions.Contains(c.Id)))
            {
                await _commandHandler.ExecuteCommand(new RemoveOrganizationMailBoxCommand(transaction),
                    new OrganizationMailBoxKeys
                    {
                        OrganizationId = organizationMailBox.Id,
                        MailBoxIds = organizationMailBox.MailBoxes.Select(p => p.Id).ToList(),
                        ModifiedBy = model.ModifiedBy,
                        ModifiedDate = model.ModifiedDate.Value
                    }, cancellationToken);
            }
        }

        private async Task RemoveClientMailBoxs(MailBox model, List<ASNClient> MailBoxClients, Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            var updateClients = model.Clients?.Select(e => e.Id).ToList() ?? new List<Guid>();

            foreach (var clientMailBox in MailBoxClients.Where(c => !updateClients.Contains(c.Id)))
            {
                await _commandHandler.ExecuteCommand(new RemoveClientMailBoxCommand(transaction),
                   new ClientMailBoxKeys
                   {
                       ClientId = clientMailBox.Id,
                       MailBoxIds = clientMailBox.MailBoxes.Select(p => p.Id).ToList(),
                       ModifiedBy = model.ModifiedBy,
                       ModifiedDate = model.ModifiedDate.Value
                   }, cancellationToken);
            }
        }

        private async Task UpdateClients(ASNClient[] clients, MailBox MailBox, CancellationToken cancellationToken)
        {
            foreach (var client in clients)
            {
                var current = await _clientService.Get(client => client, client.Id, cancellationToken);

                current.ModifiedBy = "system";
                current.ModifiedDate = DateTime.UtcNow;
                current.MailBoxes.Add(MailBox);

                await _clientService.Update(current, cancellationToken);
            }
        }

        private async Task UpdateOrganizations(Datalayer.Domain.Organization[] organizations, MailBox MailBox, CancellationToken cancellationToken)
        {
            foreach (var organization in organizations)
            {
                var current = await _organizationService.Get(org => org, organization.Id, cancellationToken);

                current.ModifiedBy = "system";
                current.ModifiedDate = DateTime.UtcNow;
                current.MailBoxes.Add(MailBox);

                await _organizationService.Update(current, cancellationToken);
            }
        }
    }


    #region Projection 
    //    public async Task<DefaultSearch<MailBox>> Query(DefaultSearch<MailBox> search, CancellationToken cancellationToken = default)
    //    {
    //        ExpressionStarter<MailBox> whereClauseBuilder = PredicateBuilder.New<MailBox>(x => true);

    //        if (search.NameSearch != null)
    //        {
    //            whereClauseBuilder.And(mailBox => mailBox.ServerUserName.Contains(search.NameSearch));
    //            whereClauseBuilder.Or(mailBox => mailBox.Server.Contains(search.NameSearch));
    //        }

    //        if (search.DescriptionSearch != null)
    //        {
    //            whereClauseBuilder.And(mailBox => mailBox.AdminEmail.Contains(search.DescriptionSearch));
    //        }

    //        if (search.CreatedDateSearchRange != null)
    //        {
    //            whereClauseBuilder.And(row => row.CreatedDate.Date >= search.CreatedDateSearchRange.FromDate.Date);
    //            whereClauseBuilder.And(row => row.CreatedDate.Date <= search.CreatedDateSearchRange.ToDate.Date);
    //        }

    //        System.Collections.Generic.ICollection<MailBox> results = await _queryHandler.SelectSortHandler(MailBoxProjection.MailBoxInformation, whereClauseBuilder, search.SortOptions?.OfType<ISortingOption>().ToList(), search, cancellationToken);

    //        search.TotalOfRecords = await _queryHandler.CountHandler<MailBox>(whereClauseBuilder, cancellationToken);

    //        search.Results = results.ToList();

    //        return search;
    //    }

    //    public async Task<MailBox> Query(MailBox model, Expression<Func<MailBox, MailBox>> projection, Expression<Func<MailBox, bool>> whereClause, CancellationToken cancellationToken = default)
    //    {
    //        return await _queryHandler.SelectHandler(projection, whereClause, cancellationToken);
    //    }
    #endregion


}





