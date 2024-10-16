using Datalayer.Domain.Group;
using System;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections
{
    public class SeriesProjection
    {
        public static Expression<Func<Series, Series>> SeriesInformation => o => new Series
        {
            Id = o.Id,
            Name = o.Name,
            Description = o.Description,
            CreatedBy = o.CreatedBy,
            CreatedDate = o.CreatedDate,
            ModifiedBy = o.ModifiedBy,
            ModifiedDate = o.ModifiedDate

        };
    }
}
