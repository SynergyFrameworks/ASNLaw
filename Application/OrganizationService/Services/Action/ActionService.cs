
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

    public class ActionService : CrudServiceBase<ASNAction>, IService<ASNAction, DefaultSearch<ASNAction>>
    {

        private readonly ICommandHandler _commandHandler;

        public ActionService(ICrudHandler<ASNDbContext> handler
            , IQueryHandler<ASNDbContext> queryHandler
            , ICommandHandler commandHandler
            
            )  : base(handler, queryHandler)
        {
            _commandHandler = commandHandler;

        }

        public async Task<DefaultSearch<ASNAction>> Query(DefaultSearch<ASNAction> search, CancellationToken cancellationToken = default)
        {
            ExpressionStarter<ASNAction> whereClauseBuilder = PredicateBuilder.New<ASNAction>(x => true);

            if (search.NameSearch != null)
            {
                whereClauseBuilder.And(ASNAction => ASNAction.Name.Contains(search.NameSearch));
                
            }

            if (search.DescriptionSearch != null)
            {
                whereClauseBuilder.And(ASNAction => ASNAction.Description.Contains(search.DescriptionSearch));
            }

            if (search.CreatedDateSearchRange != null)
            {
                whereClauseBuilder.And(row => row.CreatedDate.Date >= search.CreatedDateSearchRange.FromDate.Date);
                whereClauseBuilder.And(row => row.CreatedDate.Date <= search.CreatedDateSearchRange.ToDate.Date);
            }

            System.Collections.Generic.ICollection<ASNAction> results = await _queryHandler.SelectSortHandler(ASNActionProjection.ASNActionInformation, whereClauseBuilder, search.SortOptions?.OfType<ISortingOption>().ToList(), search, cancellationToken);

            search.TotalOfRecords = await _queryHandler.CountHandler<ASNAction>(whereClauseBuilder, cancellationToken);

            search.Results = results.ToList();

            return search;
        }

        public async Task<ASNAction> Query(Expression<Func<ASNAction, ASNAction>> projection, Expression<Func<ASNAction, bool>> whereClause, CancellationToken cancellationToken = default)
        {
            return await _queryHandler.SelectHandler(projection, whereClause, cancellationToken);
        }
    }
}
