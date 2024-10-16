
using LinqKit;
using OrganizationService.Model;
using Datalayer.Context;
using Datalayer.Domain.Storage;
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

    public class StorageProviderService : CrudServiceBase<StorageProvider>, IService<StorageProvider, DefaultSearch<StorageProviderSearchResult>>
    {
        public StorageProviderService(ICrudHandler<ASNDbContext> handler, IQueryHandler<ASNDbContext> queryHandler) : base(handler, queryHandler)
        {
        }

        public async Task<DefaultSearch<StorageProviderSearchResult>> Query(DefaultSearch<StorageProviderSearchResult> search, CancellationToken cancellationToken = default)
        {
            var whereClauseBuilder = PredicateBuilder.New<StorageProvider>(x => true);

            if (search.NameSearch != null)
            {
                whereClauseBuilder.And(group => group.DisplayName.Contains(search.NameSearch));
            }

            if (search.CreatedDateSearchRange != null)
            {
                whereClauseBuilder.And(row => row.CreatedDate.Date >= search.CreatedDateSearchRange.FromDate.Date);
                whereClauseBuilder.And(row => row.CreatedDate.Date <= search.CreatedDateSearchRange.ToDate.Date);
            }

            var results = await _queryHandler.SelectSortHandler(StorageProviderSearchDetails.StorageProviderSearch, whereClauseBuilder, search.SortOptions?.OfType<ISortingOption>().ToList(), search, cancellationToken);

            search.TotalOfRecords = await _queryHandler.CountHandler<StorageProvider>(whereClauseBuilder, cancellationToken);

            search.Results = results.ToList();

            return search;
        }

        public async Task<StorageProvider> Query(Expression<Func<StorageProvider, StorageProvider>> projection, Expression<Func<StorageProvider, bool>> whereClause, CancellationToken cancellationToken = default)
        {
            return await _queryHandler.SelectHandler(projection, whereClause, cancellationToken);
        }
    }
}
