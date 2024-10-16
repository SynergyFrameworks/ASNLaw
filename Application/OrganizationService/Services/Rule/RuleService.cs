
using LinqKit;
using OrganizationService.Model;
using Datalayer.Context;
using Datalayer.Domain.Group;
using Datalayer.Domain.Security;
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

    public class RuleService : CrudServiceBase<Permission>, IService<Permission, DefaultSearch<PermissionSearchResult>>
    {
        public RuleService(ICrudHandler<ASNDbContext> handler, IQueryHandler<ASNDbContext> queryHandler) : base(handler, queryHandler)
        {
        }

        public async Task<DefaultSearch<PermissionSearchResult>> Query(DefaultSearch<PermissionSearchResult> search, CancellationToken cancellationToken = default)
        {
            var whereClauseBuilder = PredicateBuilder.New<Permission>(x => true);

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

            var results = await _queryHandler.SelectSortHandler(PermissionSearchDetails.RuleSearch, whereClauseBuilder, search.SortOptions?.OfType<ISortingOption>().ToList(), search, cancellationToken);

            search.TotalOfRecords = await _queryHandler.CountHandler<Permission>(whereClauseBuilder, cancellationToken);

            search.Results = results.ToList();

            return search;
        }

        public async Task<Permission> Query(Expression<Func<Permission, Permission>> projection, Expression<Func<Permission, bool>> whereClause, CancellationToken cancellationToken = default)
        {
            return await _queryHandler.SelectHandler(projection, whereClause, cancellationToken);
        }
    }
}
