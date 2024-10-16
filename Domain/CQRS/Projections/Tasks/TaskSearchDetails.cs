
using Datalayer.Domain.Group;
using Infrastructure.CQRS.Models;
using System;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections
{
    public class TaskSearchDetails
    {
        public static Expression<Func<ASNTask, TaskSearchResult>> TaskSearch
        {
            get =>
                o => new TaskSearchResult
                {
                    Id = o.Id,
                    Name = o.Name,
                    Description = o.Description,
                    ShortName = o.ShortName,
                    ProjectId = o.Project.Id,
                    ModuleId = o.Module.Id,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    ModifiedBy = o.ModifiedBy,
                    ModifiedDate = o.ModifiedDate,
                    State = o.State,
                    

                };
        }
    }

}


