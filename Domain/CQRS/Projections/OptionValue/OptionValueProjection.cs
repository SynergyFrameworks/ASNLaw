using Datalayer.Domain.Group;
using System;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections
{
    public class OptionValueProjection
    {
        public static Expression<Func<OptionValue, OptionValue>> OptionValueInformation
        {
            get =>
                o => new OptionValue
                {
                    Id = o.Id,
                    Value = o.Value,
                    DataType = o.DataType,
                    DisplayText = o.DisplayText,              
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    ModifiedBy = o.ModifiedBy,
                    ModifiedDate = o.ModifiedDate,
                };
        }
    }
}
