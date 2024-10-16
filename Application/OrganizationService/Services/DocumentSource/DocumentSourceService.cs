
using LinqKit;
using OrganizationService.Model;
using Datalayer.Context;
using Datalayer.Domain.Group;
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

    public class DocumentSourceService : CrudServiceBase<DocumentSource>, IService<DocumentSource, DefaultSearch<DocumentSource>>
    {
        public DocumentSourceService(ICrudHandler<ASNDbContext> handler, IQueryHandler<ASNDbContext> queryHandler) : base(handler, queryHandler)
        {
        }

        public async Task<DefaultSearch<DocumentSource>> Query(DefaultSearch<DocumentSource> search, CancellationToken cancellationToken = default)
        {
            var whereClauseBuilder = PredicateBuilder.New<DocumentSource>(x => true);

            if (search.NameSearch != null)
            {
                whereClauseBuilder.And(documentSource => documentSource.InputFolder.Contains(search.NameSearch));
                whereClauseBuilder.Or(documentSource => documentSource.OutputFolder.Contains(search.NameSearch));
            }

            if (search.DescriptionSearch != null)
            {
                whereClauseBuilder.And(documentSource => documentSource.Description.Contains(search.DescriptionSearch));
            }

            if (search.CreatedDateSearchRange != null)
            {
                whereClauseBuilder.And(row => row.CreatedDate.Date >= search.CreatedDateSearchRange.FromDate.Date);
                whereClauseBuilder.And(row => row.CreatedDate.Date <= search.CreatedDateSearchRange.ToDate.Date);
            }

            var results = await _queryHandler.SelectSortHandler(DocumentSourceProjection.DocumentSourceInformation, whereClauseBuilder, search.SortOptions?.OfType<ISortingOption>().ToList(), search, cancellationToken);

            search.TotalOfRecords = await _queryHandler.CountHandler<DocumentSource>(whereClauseBuilder, cancellationToken);

            search.Results = results.ToList();

            return search;
        }

        public async Task<DocumentSource> Query(Expression<Func<DocumentSource, DocumentSource>> projection, Expression<Func<DocumentSource, bool>> whereClause, CancellationToken cancellationToken = default)
        {
            return await _queryHandler.SelectHandler(projection, whereClause, cancellationToken);
        }
    }
}
