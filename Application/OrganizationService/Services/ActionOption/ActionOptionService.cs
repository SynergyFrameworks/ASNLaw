
using LinqKit;
using OrganizationService.Model;
using Datalayer.Context;
using Datalayer.Domain.Group;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Projections;
using Infrastructure.Common.Sorting;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.Services
{

    public class ActionOptionService : CrudServiceBase<ActionOption>, IService<ActionOption, DefaultSearch<ActionOption>>
    {

        private readonly ICommandHandler _commandHandler;
        public ActionOptionService(ICrudHandler<ASNDbContext> handler
            , IQueryHandler<ASNDbContext> queryHandler
            , ICommandHandler commandHandler

            ) : base(handler, queryHandler)
        {
            _commandHandler = commandHandler;

        }

        public async Task<DefaultSearch<ActionOption>> Query(DefaultSearch<ActionOption> search, CancellationToken cancellationToken = default)
        {
            ExpressionStarter<ActionOption> whereClauseBuilder = PredicateBuilder.New<ActionOption>(x => true);

            if (search.NameSearch != null)
            {
                whereClauseBuilder.And(ASNAction => ASNAction.Label.Contains(search.NameSearch));
                
            }

            if (search.CreatedDateSearchRange != null)
            {
                whereClauseBuilder.And(row => row.CreatedDate.Date >= search.CreatedDateSearchRange.FromDate.Date);
                whereClauseBuilder.And(row => row.CreatedDate.Date <= search.CreatedDateSearchRange.ToDate.Date);
            }

            System.Collections.Generic.ICollection<ActionOption> results = await _queryHandler.SelectSortHandler(ActionOptionProjection.ActionOptionInformation, whereClauseBuilder, search.SortOptions?.OfType<ISortingOption>().ToList(), search, cancellationToken);

            search.TotalOfRecords = await _queryHandler.CountHandler<ActionOption>(whereClauseBuilder, cancellationToken);

            search.Results = results.ToList();

            return search;
        }

        public async Task<ActionOption> Query(Expression<Func<ActionOption, ActionOption>> projection, Expression<Func<ActionOption, bool>> whereClause, CancellationToken cancellationToken = default)
        {
            return await _queryHandler.SelectHandler(projection, whereClause, cancellationToken);
        }
    }
}
