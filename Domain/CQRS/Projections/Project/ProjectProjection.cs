using Datalayer.Domain.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Projections
{
    public class ProjectProjection
    {
        public static Expression<Func<Project, Project>> ProjectInformation
        {
            get =>
                o => new Project
                {
                    Id = o.Id,
                    Name = o.Name,
                    Description = o.Description,
                    GroupId = o.GroupId,
                    ASNGroup = o.ASNGroup,
                    ImageUrl = o.ImageUrl,
                };
        }
    }
}



