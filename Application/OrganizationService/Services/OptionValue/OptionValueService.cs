
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

    public class OptionValueService : CrudServiceBase<OptionValue>, IService<OptionValue, DefaultSearch<OptionValue>>
    {

        private readonly ICommandHandler _commandHandler;
        public OptionValueService(ICrudHandler<ASNDbContext> handler
            , IQueryHandler<ASNDbContext> queryHandler
            , ICommandHandler commandHandler

            ) : base(handler, queryHandler)
        {
            _commandHandler = commandHandler;

        }

        public async Task<DefaultSearch<OptionValue>> Query(DefaultSearch<OptionValue> search, CancellationToken cancellationToken = default)
        {
            ExpressionStarter<OptionValue> whereClauseBuilder = PredicateBuilder.New<OptionValue>(x => true);

            if (search.NameSearch != null)
            {
                whereClauseBuilder.And(option => option.DisplayText.Contains(search.NameSearch));
                
            }

            if (search.CreatedDateSearchRange != null)
            {
                whereClauseBuilder.And(row => row.CreatedDate.Date >= search.CreatedDateSearchRange.FromDate.Date);
                whereClauseBuilder.And(row => row.CreatedDate.Date <= search.CreatedDateSearchRange.ToDate.Date);
            }

            System.Collections.Generic.ICollection<OptionValue> results = await _queryHandler.SelectSortHandler(OptionValueProjection.OptionValueInformation, whereClauseBuilder, search.SortOptions?.OfType<ISortingOption>().ToList(), search, cancellationToken);

            search.TotalOfRecords = await _queryHandler.CountHandler<OptionValue>(whereClauseBuilder, cancellationToken);

            search.Results = results.ToList();

            return search;
        }

        public async Task<OptionValue> Query(Expression<Func<OptionValue, OptionValue>> projection, Expression<Func<OptionValue, bool>> whereClause, CancellationToken cancellationToken = default)
        {
            return await _queryHandler.SelectHandler(projection, whereClause, cancellationToken);
        }
    }
}
