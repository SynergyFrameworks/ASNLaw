using Datalayer.Domain.Group;
using Infrastructure.CQRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Projections
{
    public class ResourceSearchDetails
    {
        public static Expression<Func<Resource, ResourceSearchResult>> ResourceSearch
        {
            get =>
                o => new ResourceSearchResult
                {
                    Id = o.Id,
                    ResourceType = o.ResourceType,
                    JsonValues = o.JsonValues,
                    ModuleId = o.ModuleId,
                    Module = o.Module,
                    State = o.State,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    ModifiedBy = o.ModifiedBy,
                    ModifiedDate = o.ModifiedDate,
                };
        }
    }
}
