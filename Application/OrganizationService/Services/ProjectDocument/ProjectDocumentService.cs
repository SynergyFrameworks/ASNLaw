
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

    public class ProjectDocumentService : CrudServiceBase<ProjectDocument>, IService<ProjectDocument, DefaultSearch<ProjectDocument>>
    {
        public ProjectDocumentService(ICrudHandler<ASNDbContext> handler, IQueryHandler<ASNDbContext> queryHandler) : base(handler, queryHandler)
        {
        }

        public async Task<DefaultSearch<ProjectDocument>> Query(DefaultSearch<ProjectDocument> search, CancellationToken cancellationToken = default)
        {
            var whereClauseBuilder = PredicateBuilder.New<ProjectDocument>(x => true);

            if (search.NameSearch != null)
            {
                whereClauseBuilder.And(projectdocument => projectdocument.Name.Contains(search.NameSearch));
            }

            if (search.DescriptionSearch != null)
            {
                whereClauseBuilder.And(projectdocument => projectdocument.Description.Contains(search.DescriptionSearch));
            }

            if (search.CreatedDateSearchRange != null)
            {
                whereClauseBuilder.And(row => row.CreatedDate.Date >= search.CreatedDateSearchRange.FromDate.Date);
                whereClauseBuilder.And(row => row.CreatedDate.Date <= search.CreatedDateSearchRange.ToDate.Date);
            }

            var results = await _queryHandler.SelectSortHandler(ProjectDocumentProjection.ProjectDocumentInformation, whereClauseBuilder, search.SortOptions?.OfType<ISortingOption>().ToList(), search, cancellationToken);

            search.TotalOfRecords = await _queryHandler.CountHandler<ProjectDocument>(whereClauseBuilder, cancellationToken);

            search.Results = results.ToList();

            return search;
        }


        public async Task<ProjectDocument> Query(Expression<Func<ProjectDocument, ProjectDocument>> projection, Expression<Func<ProjectDocument, bool>> whereClause, CancellationToken cancellationToken = default)
        {
            return await _queryHandler.SelectHandler(projection, whereClause, cancellationToken);
        }
    }
}
