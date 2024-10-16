using Datalayer.Domain.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Projections
{
    public class ResourceProjection
    {
        public static Expression<Func<Resource, Resource>> ResourceInformation
        {
            get =>
                o => new Resource
                {
                    Id = o.Id,
                    ResourceType = o.ResourceType,
                    JsonValues = o.JsonValues,
                    ModuleId = o.ModuleId,
                    Module = o.Module
                };
        }
    }
}
