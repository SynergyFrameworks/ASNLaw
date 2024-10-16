using Datalayer.Context;
using Infrastructure.Common.Sorting;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models;
using Infrastructure.CQRS.Projections;
using LinqKit;
using OrganizationService.Model;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Datalayer.Domain;

namespace Organization.Services
{
    public class OrganizationService : CrudServiceBase<Datalayer.Domain.Organization>, IService<Datalayer.Domain.Organization, DefaultSearch<Datalayer.Domain.Organization>>
    {
        public OrganizationService(ICrudHandler<ASNDbContext> handler, IQueryHandler<ASNDbContext> queryHandler) : base(handler, queryHandler)
        {
        }

        public async Task<Datalayer.Domain.Organization> Query(DefaultSearch<Datalayer.Domain.Organization> searchCriteria, CancellationToken cancellationToken = default)
        {
            // Assuming you use the searchCriteria in some way for the query
            Expression<Func<Datalayer.Domain.Organization, Datalayer.Domain.Organization>> projection = o => o;
            Expression<Func<Datalayer.Domain.Organization, bool>> whereClause = o => true;  // Modify this to your actual filtering logic

            return await _queryHandler.SelectHandler(projection, whereClause, cancellationToken);
        }
        public async Task<DefaultSearch<OrganizationSearchResult>> Query(DefaultSearch<OrganizationSearchResult> search, CancellationToken cancellationToken = default)
        {
            var whereClauseBuilder = PredicateBuilder.New<Datalayer.Domain.Organization>(x => true);

            if (search.NameSearch != null)
            {
                whereClauseBuilder.And(org => org.Name.Contains(search.NameSearch));
            }

            if (search.DescriptionSearch != null)
            {
                whereClauseBuilder.And(org => org.Description.Contains(search.DescriptionSearch));
            }

            if (search.CreatedDateSearchRange != null)
            {
                whereClauseBuilder.And(row => row.CreatedDate.Date >= search.CreatedDateSearchRange.FromDate.Date);
                whereClauseBuilder.And(row => row.CreatedDate.Date <= search.CreatedDateSearchRange.ToDate.Date);
            }

            var results = await _queryHandler.SelectSortHandler(OrganizationSearchDetails.OrganizationSearch, whereClauseBuilder, search.SortOptions?.OfType<ISortingOption>().ToList(), search, cancellationToken);

            search.TotalOfRecords = await _queryHandler.CountHandler<Datalayer.Domain.Organization>(whereClauseBuilder, cancellationToken);

            search.Results = results.ToList();

            return search;
        }

        public Task<Datalayer.Domain.Organization> Query(Expression<Func<Datalayer.Domain.Organization, Datalayer.Domain.Organization>> projection, Expression<Func<Datalayer.Domain.Organization, bool>> whereClause, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        Task<DefaultSearch<Datalayer.Domain.Organization>> IService<Datalayer.Domain.Organization, DefaultSearch<Datalayer.Domain.Organization>>.Query(DefaultSearch<Datalayer.Domain.Organization> search, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
