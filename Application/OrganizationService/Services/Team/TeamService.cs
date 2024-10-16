
using LinqKit;
using OrganizationService.Model;
using Datalayer.Context;
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
    public class TeamService : CrudServiceBase<Team>, IService<Team, DefaultSearch<TeamSearchResult>>
    {
        public TeamService(ICrudHandler<ASNDbContext> handler, IQueryHandler<ASNDbContext> queryHandler) : base(handler, queryHandler)
        {
        }

        public async Task<DefaultSearch<TeamSearchResult>> Query(DefaultSearch<TeamSearchResult> search, CancellationToken cancellationToken = default)
        {
            ExpressionStarter<Team> whereClauseBuilder = PredicateBuilder.New<Team>(x => true);

            if (search.NameSearch != null)
            {
                whereClauseBuilder.And(Team => Team.Name.Contains(search.NameSearch));
            }

            if (search.DescriptionSearch != null)
            {
                whereClauseBuilder.And(Team => Team.Description.Contains(search.DescriptionSearch));
            }

            if (search.CreatedDateSearchRange != null)
            {
                whereClauseBuilder.And(row => row.CreatedDate.Date >= search.CreatedDateSearchRange.FromDate.Date);
                whereClauseBuilder.And(row => row.CreatedDate.Date <= search.CreatedDateSearchRange.ToDate.Date);
            }

            ICollection<TeamSearchResult> results = await _queryHandler.SelectSortHandler(TeamSearchDetails.TeamSearch, whereClauseBuilder, search.SortOptions?.OfType<ISortingOption>().ToList(), search, cancellationToken);

            search.TotalOfRecords = await _queryHandler.CountHandler<Team>(whereClauseBuilder, cancellationToken);

            search.Results = results.ToList();

            return search;
        }

        public async Task<Team> Query(Expression<Func<Team, Team>> projection, Expression<Func<Team, bool>> whereClause, CancellationToken cancellationToken = default)
        {
            return await _queryHandler.SelectHandler(projection, whereClause, cancellationToken);
        }
    }
}
