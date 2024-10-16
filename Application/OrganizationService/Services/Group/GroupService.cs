
using LinqKit;
using OrganizationService.Model;
using Datalayer.Context;
using Datalayer.Domain.Group;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Models;
using Infrastructure.CQRS.Projections;
using Infrastructure.Common.Sorting;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.Services
{

    public class GroupService : CrudServiceBase<ASNGroup>, IService<ASNGroup, DefaultSearch<GroupSearchResult>>
    {
        public GroupService(ICrudHandler<ASNDbContext> handler, IQueryHandler<ASNDbContext> queryHandler) : base(handler, queryHandler)
        {
        }

        public async Task<DefaultSearch<GroupSearchResult>> Query(DefaultSearch<GroupSearchResult> search, CancellationToken cancellationToken = default)
        {
            var whereClauseBuilder = PredicateBuilder.New<ASNGroup>(x => true);

            if (search.NameSearch != null)
            {
                whereClauseBuilder.And(group => group.Name.Contains(search.NameSearch));
            }

            if (search.DescriptionSearch != null)
            {
                whereClauseBuilder.And(group => group.Description.Contains(search.DescriptionSearch));
            }

            if (search.CreatedDateSearchRange != null)
            {
                whereClauseBuilder.And(row => row.CreatedDate.Date >= search.CreatedDateSearchRange.FromDate.Date);
                whereClauseBuilder.And(row => row.CreatedDate.Date <= search.CreatedDateSearchRange.ToDate.Date);
            }

            var results = await _queryHandler.SelectSortHandler(GroupSearchDetails.GroupSearch, whereClauseBuilder, search.SortOptions?.OfType<ISortingOption>().ToList(), search, cancellationToken);

            search.TotalOfRecords = await _queryHandler.CountHandler<ASNGroup>(whereClauseBuilder, cancellationToken);

            search.Results = results.ToList();

            return search;
        }

        public async Task<ASNGroup> Query(Expression<Func<ASNGroup, ASNGroup>> projection, Expression<Func<ASNGroup, bool>> whereClause, CancellationToken cancellationToken = default)
        {
            return await _queryHandler.SelectHandler(projection, whereClause, cancellationToken);
        }
    }
}
