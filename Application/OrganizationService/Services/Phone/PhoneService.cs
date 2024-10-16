
using LinqKit;
using OrganizationService.Model;
using Datalayer.Context;
using Datalayer.Domain.Demographics;
using Datalayer.Domain;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models;
using Infrastructure.CQRS.Projections;
using Infrastructure.CQRS.Commands;
using Infrastructure.Common.Sorting;
using Infrastructure.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.CQRS.Models.Phone;


namespace Organization.Services
{
    public class PhoneService : CrudServiceBase<Phone>, IService<Phone, DefaultSearch<PhoneSearchResult>>
    {
        private readonly IService<Datalayer.Domain.Organization, DefaultSearch<OrganizationSearchResult>> _organizationService;
        private readonly IService<ASNClient, DefaultSearch<ClientSearchResult>> _clientService;
        private readonly ICommandHandler _commandHandler;

        public PhoneService(ICrudHandler<ASNDbContext> handler
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

        public override async Task<Phone> Add(Phone model, CancellationToken cancellationToken = default)
        {
            var organizations = model.Organizations?.ToArray() ?? new Datalayer.Domain.Organization[] { };
            var clients = model.Clients?.ToArray() ?? new ASNClient[] { };

            model.Clients = new HashSet<ASNClient>();
            model.Organizations = new HashSet<Datalayer.Domain.Organization>();

            var phone = await base.Add(model, cancellationToken);

            try
            {
                await UpdateOrganizations(organizations, phone, cancellationToken);
                await UpdateClients(clients, phone, cancellationToken);
            }
            catch (Exception)
            {
                await base.Delete(phone);
                throw;
            }

            phone.Organizations = phone.Organizations.Select(org => new Datalayer.Domain.Organization { Id = org.Id, Name = org.Name }).ToArray();
            phone.Clients = phone.Clients.Select(client => new ASNClient { Id = client.Id, Name = client.Name, ClientNo = client.ClientNo }).ToArray();

            return phone;
        }

        public override async Task<Phone> Update(Phone model, CancellationToken cancellationToken = default)
        {
            using (var transaction = await _commandHandler.CreateTransaction(cancellationToken))
            {
                var currentPhone = await _commandHandler.ExecuteCommand(new UpdatePhoneCommand(transaction), model, cancellationToken);

                var currrentPhoneClients = currentPhone.Clients.ToList();
                var currentPhoneOrganizations = currentPhone.Organizations.ToList();

                await RemoveClientPhones(model, currrentPhoneClients, transaction, cancellationToken);
                await RemoveOrganizationPhones(model, currentPhoneOrganizations, transaction, cancellationToken);

                currentPhone.Organizations = currentPhone.Organizations.Select(org => new Datalayer.Domain.Organization { Id = org.Id, Name = org.Name }).ToArray();
                currentPhone.Clients = currentPhone.Clients.Select(client => new ASNClient { Id = client.Id, Name = client.Name, ClientNo = client.ClientNo }).ToArray();

                await transaction.CommitAsync(cancellationToken);

                return currentPhone;
            }
        }

        public async Task<DefaultSearch<PhoneSearchResult>> Query(DefaultSearch<PhoneSearchResult> search, CancellationToken cancellationToken = default)
        {
            var whereClauseBuilder = PredicateBuilder.New<Phone>(x => true);

            if (!String.IsNullOrWhiteSpace(search.NameSearch))
            {
                whereClauseBuilder.And(phone => phone.PhoneNumber.Contains(search.NameSearch));
                whereClauseBuilder.Or(phone => phone.Organizations.Count(o => o.Name.Contains(search.NameSearch)) > 0);
                whereClauseBuilder.Or(phone => phone.Clients.Count(o => o.Name.Contains(search.NameSearch)) > 0);
            }

            if (search.CreatedDateSearchRange != null)
            {
                whereClauseBuilder.And(row => row.CreatedDate.Date >= search.CreatedDateSearchRange.FromDate.Date);
                whereClauseBuilder.And(row => row.CreatedDate.Date <= search.CreatedDateSearchRange.ToDate.Date);
            }

            var results = await _queryHandler.SelectSortHandler(PhoneSearchDetails.PhoneSearch, whereClauseBuilder, search.SortOptions?.OfType<ISortingOption>().ToList(), search, cancellationToken);

            search.TotalOfRecords = await _queryHandler.CountHandler<Phone>(whereClauseBuilder, cancellationToken);

            search.Results = results.ToList();

            return search;
        }

        public async Task<Phone> Query(Expression<Func<Phone, Phone>> projection, Expression<Func<Phone, bool>> whereClause, CancellationToken cancellationToken = default)
        {
            return await _queryHandler.SelectHandler(projection, whereClause, cancellationToken);
        }

        private async Task RemoveOrganizationPhones(Phone model, List<Datalayer.Domain.Organization> phoneOrganizations, Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            var updateOrganizanitions = model.Organizations?.Select(e => e.Id).ToList() ?? new List<Guid>();

            foreach (var organizationPhone in phoneOrganizations.Where(c => !updateOrganizanitions.Contains(c.Id)))
            {
                await _commandHandler.ExecuteCommand(new RemoveOrganizationPhonesCommand(transaction),
                    new OrganizationPhoneKeys
                    {
                        OrganizationId = organizationPhone.Id,
                        PhoneIds = organizationPhone.Phones.Select(p => p.Id).ToList(),
                        ModifiedBy = model.ModifiedBy,
                        ModifiedDate = model.ModifiedDate.Value
                    }, cancellationToken);
            }
        }

        private async Task RemoveClientPhones(Phone model, List<ASNClient> phoneClients, Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            var updateClients = model.Clients?.Select(e => e.Id).ToList() ?? new List<Guid>();

            foreach (var clientPhone in phoneClients.Where(c => !updateClients.Contains(c.Id)))
            {
                await _commandHandler.ExecuteCommand(new RemoveClientPhonesCommand(transaction),
                   new ClientPhoneKeys
                   {
                       ClientId = clientPhone.Id,
                       PhoneIds = clientPhone.Phones.Select(p => p.Id).ToList(),
                       ModifiedBy = model.ModifiedBy,
                       ModifiedDate = model.ModifiedDate.Value
                   }, cancellationToken);
            }
        }

        private async Task UpdateClients(ASNClient[] clients, Phone phone, CancellationToken cancellationToken)
        {
            foreach (var client in clients)
            {
                var current = await _clientService.Get(client => client, client.Id, cancellationToken);

                current.ModifiedBy = "system";
                current.ModifiedDate = DateTime.UtcNow;
                current.Phones.Add(phone);

                await _clientService.Update(current, cancellationToken);
            }
        }

        private async Task UpdateOrganizations(Datalayer.Domain.Organization[] organizations, Phone phone, CancellationToken cancellationToken)
        {
            foreach (var organization in organizations)
            {
                var current = await _organizationService.Get(org => org, organization.Id, cancellationToken);

                current.ModifiedBy = "system";
                current.ModifiedDate = DateTime.UtcNow;
                current.Phones.Add(phone);

                await _organizationService.Update(current, cancellationToken);
            }
        }
    }
}
