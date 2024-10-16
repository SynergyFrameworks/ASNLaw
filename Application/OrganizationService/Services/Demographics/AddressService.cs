
using LinqKit;
using OrganizationService.Model;
using Datalayer.Context;
using Datalayer.Domain.Demographics;
using Datalayer.Domain;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models;
using Infrastructure.Common.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.CQRS.Models.Addresses;
using Infrastructure.CQRS.Commands;
using Infrastructure.CQRS.Projections.Addresses;

namespace Organization.Services
{
    public class AddressService : CrudServiceBase<Address>, IService<Address, DefaultSearch<AddressSearchResult>>
    {
        private readonly IService<Datalayer.Domain.Organization, DefaultSearch<OrganizationSearchResult>> _organizationService;
        private readonly IService<ASNClient, DefaultSearch<ClientSearchResult>> _clientService;
        private readonly ICommandHandler _commandHandler;

        public AddressService(ICrudHandler<ASNDbContext> handler
            , IQueryHandler<ASNDbContext> queryHandler
            , IService<Datalayer.Domain.Organization, DefaultSearch<OrganizationSearchResult>> organizationService
            , IService<ASNClient, DefaultSearch<ClientSearchResult>> clientService
            , ICommandHandler commandHandler

            ) : base(handler, queryHandler)
        {
            _organizationService = organizationService;
            _clientService = clientService;
            _commandHandler = commandHandler;
        }

        public override async Task<Address> Add(Address model, CancellationToken cancellationToken = default)
        {
            var organizations = model.Organizations?.ToArray() ?? new Datalayer.Domain.Organization[] { };
            var clients = model.Clients?.ToArray() ?? new ASNClient[] { };

            model.Clients = new HashSet<ASNClient>();
            model.Organizations = new HashSet<Datalayer.Domain.Organization>();

            var Address = await base.Add(model, cancellationToken);

            try
            {
                await UpdateOrganizations(organizations, Address, cancellationToken);
                await UpdateClients(clients, Address, cancellationToken);
            }
            catch (Exception)
            {
                await base.Delete(Address);
                throw;
            }

            Address.Organizations = Address.Organizations.Select(org => new Datalayer.Domain.Organization { Id = org.Id, Name = org.Name }).ToArray();
            Address.Clients = Address.Clients.Select(client => new ASNClient { Id = client.Id, Name = client.Name, ClientNo = client.ClientNo }).ToArray();

            return Address;
        }

        public override async Task<Address> Update(Address model, CancellationToken cancellationToken = default)
        {
            using (var transaction = await _commandHandler.CreateTransaction(cancellationToken))
            {
                var currentAddress = await _commandHandler.ExecuteCommand(new UpdateAddressCommand(transaction), model, cancellationToken);

                var currrentAddressClients = currentAddress.Clients.ToList();
                var currentAddressOrganizations = currentAddress.Organizations.ToList();

                await RemoveClientAddresss(model, currrentAddressClients, transaction, cancellationToken);
                await RemoveOrganizationAddresss(model, currentAddressOrganizations, transaction, cancellationToken);

                currentAddress.Organizations = currentAddress.Organizations.Select(org => new Datalayer.Domain.Organization { Id = org.Id, Name = org.Name }).ToArray();
                currentAddress.Clients = currentAddress.Clients.Select(client => new ASNClient { Id = client.Id, Name = client.Name, ClientNo = client.ClientNo }).ToArray();

                await transaction.CommitAsync(cancellationToken);

                return currentAddress;
            }
        }

        public async Task<DefaultSearch<AddressSearchResult>> Query(DefaultSearch<AddressSearchResult> search, CancellationToken cancellationToken = default)
        {
            var whereClauseBuilder = PredicateBuilder.New<Address>(x => true);

            if (!String.IsNullOrWhiteSpace(search.NameSearch))
            {
                whereClauseBuilder.Or(Address => search.NameSearch.Contains(Address.AddressLine1));
                whereClauseBuilder.Or(Address => search.NameSearch.Contains(Address.AddressLine2));
                whereClauseBuilder.Or(Address => search.NameSearch.Contains(Address.City));
                whereClauseBuilder.Or(Address => search.NameSearch.Contains(Address.StateCode));
                whereClauseBuilder.Or(Address => search.NameSearch.Contains(Address.PostalCode));
                whereClauseBuilder.Or(Address => search.NameSearch.Contains(Address.CountryCode));
                whereClauseBuilder.Or(Address => Address.Organizations.Count(o => o.Name.Contains(search.NameSearch)) > 0);
                whereClauseBuilder.Or(Address => Address.Clients.Count(o => o.Name.Contains(search.NameSearch)) > 0);
            }

            if (search.CreatedDateSearchRange != null)
            {
                whereClauseBuilder.And(row => row.CreatedDate.Date >= search.CreatedDateSearchRange.FromDate.Date);
                whereClauseBuilder.And(row => row.CreatedDate.Date <= search.CreatedDateSearchRange.ToDate.Date);
            }

            var results = await _queryHandler.SelectSortHandler(AddressSearchDetails.AddressSearch, whereClauseBuilder, search.SortOptions?.OfType<ISortingOption>().ToList(), search, cancellationToken);

            search.TotalOfRecords = await _queryHandler.CountHandler<Address>(whereClauseBuilder, cancellationToken);

            search.Results = results.ToList();

            return search;
        }

        public async Task<Address> Query(Expression<Func<Address, Address>> projection, Expression<Func<Address, bool>> whereClause, CancellationToken cancellationToken = default)
        {
            return await _queryHandler.SelectHandler(projection, whereClause, cancellationToken);
        }

        private async Task RemoveOrganizationAddresss(Address model, List<Datalayer.Domain.Organization> AddressOrganizations, Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            var updateOrganizanitions = model.Organizations?.Select(e => e.Id).ToList() ?? new List<Guid>();

            foreach (var organizationAddress in AddressOrganizations.Where(c => !updateOrganizanitions.Contains(c.Id)))
            {
                await _commandHandler.ExecuteCommand(new RemoveOrganizationAddressCommand(transaction),
                    new OrganizationAddressKeys
                    {
                        OrganizationId = organizationAddress.Id,
                        AddressIds = organizationAddress.Addresses.Select(p => p.Id).ToList(),
                        ModifiedBy = model.ModifiedBy,
                        ModifiedDate = model.ModifiedDate.Value
                    }, cancellationToken);
            }
        }

        private async Task RemoveClientAddresss(Address model, List<ASNClient> AddressClients, Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            var updateClients = model.Clients?.Select(e => e.Id).ToList() ?? new List<Guid>();

            foreach (var clientAddress in AddressClients.Where(c => !updateClients.Contains(c.Id)))
            {
                await _commandHandler.ExecuteCommand(new RemoveClientAddressCommand(transaction),
                   new ClientAddressKeys
                   {
                       ClientId = clientAddress.Id,
                       AddressIds = clientAddress.Addresses.Select(p => p.Id).ToList(),
                       ModifiedBy = model.ModifiedBy,
                       ModifiedDate = model.ModifiedDate.Value
                   }, cancellationToken);
            }
        }

        private async Task UpdateClients(ASNClient[] clients, Address Address, CancellationToken cancellationToken)
        {
            foreach (var client in clients)
            {
                var current = await _clientService.Get(client => client, client.Id, cancellationToken);

                current.ModifiedBy = "system";
                current.ModifiedDate = DateTime.UtcNow;
                current.Addresses.Add(Address);

                await _clientService.Update(current, cancellationToken);
            }
        }

        private async Task UpdateOrganizations(Datalayer.Domain.Organization[] organizations, Address Address, CancellationToken cancellationToken)
        {
            foreach (var organization in organizations)
            {
                var current = await _organizationService.Get(org => org, organization.Id, cancellationToken);

                current.ModifiedBy = "system";
                current.ModifiedDate = DateTime.UtcNow;
                current.Addresses.Add(Address);

                await _organizationService.Update(current, cancellationToken);
            }
        }
    }
}
