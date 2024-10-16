using Datalayer.Domain.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ASNCapTask = Datalayer.Domain.Group.ASNTask;

namespace Infrastructure.CQRS.Projections
{
    public class TaskProjection
    {
        public static Expression<Func<ASNCapTask, ASNCapTask>> TaskInformation
        {
            get =>
                o => new ASNCapTask
                {
                    Id = o.Id,                                     
                    Name = o.Name,
                    Description = o.Description,
                    ShortName = o.ShortName,
                    ProjectId = o.ProjectId,
                    Project = o.Project,
                    ModuleId = o.ModuleId,
                    Module = o.Module,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    ModifiedBy = o.ModifiedBy,
                    ModifiedDate = o.ModifiedDate



                };
        }
    }
}



