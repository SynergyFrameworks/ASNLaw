using Datalayer.Domain.Group;
using Infrastructure.CQRS.Models;
using System;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections
{
    public class SeriesSearchDetails
    {
        public static Expression<Func<Series, SeriesSearchResult>> SeriesSearch => o => new SeriesSearchResult
        {
            Id = o.Id,
            Name = o.Name,
            Description = o.Description,
            CreatedBy = o.CreatedBy,
            CreatedDate = o.CreatedDate,
            ModifiedBy = o.ModifiedBy,
            ModifiedDate = o.ModifiedDate,

        };
    }

}
