using Datalayer.Domain.Group;
using System;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections
{
    public class ActionOptionProjection
    {
        public static Expression<Func<ActionOption, ActionOption>> ActionOptionInformation
        {
            get =>
                o => new ActionOption
                {
                    Id = o.Id,
                    Label = o.Label,
                    Action = o.Action,
                    OptionValues = o.OptionValues,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    ModifiedBy = o.ModifiedBy,
                    ModifiedDate = o.ModifiedDate,
                };
        }
    }
}
