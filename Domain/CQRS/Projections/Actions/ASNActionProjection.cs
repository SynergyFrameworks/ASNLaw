using Datalayer.Domain.Group;
using System;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections
{
    public class ASNActionProjection
    {
        public static Expression<Func<ASNAction, ASNAction>> ASNActionInformation
        {
            get =>
                o => new ASNAction
                {
                    Id = o.Id,
                    Name= o.Name,
                    Description = o.Description,
                    ActionOptions = o.ActionOptions,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    ModifiedBy = o.ModifiedBy,
                    ModifiedDate = o.ModifiedDate,
                };
        }
    }
}
