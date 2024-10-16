
using LinqKit;
using OrganizationService.Model;
using Datalayer.Context;
using Datalayer.Domain.Group;
using Datalayer.Domain;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models;
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
    public class ResourceService : CrudServiceBase<Resource>, IService<Resource, DefaultSearch<ResourceSearchResult>>
    {

        public ResourceService(ICrudHandler<ASNDbContext> handler, IQueryHandler<ASNDbContext> queryHandler) : base(handler, queryHandler)
        {
        }

        public async Task<DefaultSearch<ResourceSearchResult>> Query(DefaultSearch<ResourceSearchResult> search, CancellationToken cancellationToken = default)
        {
            ExpressionStarter<Resource> whereClauseBuilder = PredicateBuilder.New<Resource>(x => true);

            if (search.NameSearch != null)
            {
                whereClauseBuilder.And(Resource => Resource.ResourceType.Contains(search.NameSearch));
            }


            if (search.CreatedDateSearchRange != null)
            {
                whereClauseBuilder.And(row => row.CreatedDate.Date >= search.CreatedDateSearchRange.FromDate.Date);
                whereClauseBuilder.And(row => row.CreatedDate.Date <= search.CreatedDateSearchRange.ToDate.Date);
            }

            ICollection<ResourceSearchResult> results = await _queryHandler.SelectSortHandler(ResourceSearchDetails.ResourceSearch, whereClauseBuilder, search.SortOptions?.OfType<ISortingOption>().ToList(), search, cancellationToken);

            search.TotalOfRecords = await _queryHandler.CountHandler<Resource>(whereClauseBuilder, cancellationToken);

            search.Results = results.ToList();

            return search;
        }

        public async Task<Resource> Query(Expression<Func<Resource, Resource>> projection, Expression<Func<Resource, bool>> whereClause, CancellationToken cancellationToken = default)
        {
            return await _queryHandler.SelectHandler(projection, whereClause, cancellationToken);
        }
    }
}
