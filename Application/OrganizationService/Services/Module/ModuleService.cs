
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
    public class ModuleService : CrudServiceBase<Module>, IService<Module, DefaultSearch<ModuleSearchResult>>
    {

        public ModuleService(ICrudHandler<ASNDbContext> handler, IQueryHandler<ASNDbContext> queryHandler) : base(handler, queryHandler)
        {
        }

        public async Task<DefaultSearch<ModuleSearchResult>> Query(DefaultSearch<ModuleSearchResult> search, CancellationToken cancellationToken = default)
        {
            ExpressionStarter<Module> whereClauseBuilder = PredicateBuilder.New<Module>(x => true);

            if (search.NameSearch != null)
            {
                whereClauseBuilder.And(Module => Module.Title.Contains(search.NameSearch));
            }

            if (search.DescriptionSearch != null)
            {
                whereClauseBuilder.And(Module => Module.Description.Contains(search.DescriptionSearch));
            }

            if (search.CreatedDateSearchRange != null)
            {
                whereClauseBuilder.And(row => row.CreatedDate.Date >= search.CreatedDateSearchRange.FromDate.Date);
                whereClauseBuilder.And(row => row.CreatedDate.Date <= search.CreatedDateSearchRange.ToDate.Date);
            }

            ICollection<ModuleSearchResult> results = await _queryHandler.SelectSortHandler(ModuleSearchDetails.ModuleSearch, whereClauseBuilder, search.SortOptions?.OfType<ISortingOption>().ToList(), search, cancellationToken);

            search.TotalOfRecords = await _queryHandler.CountHandler<Module>(whereClauseBuilder, cancellationToken);

            search.Results = results.ToList();

            return search;
        }

        public async Task<Module> Query(Expression<Func<Module, Module>> module, Expression<Func<Module, bool>> whereClause, CancellationToken cancellationToken = default)
        {
            return await _queryHandler.SelectHandler(module, whereClause, cancellationToken);
        }
    }
}