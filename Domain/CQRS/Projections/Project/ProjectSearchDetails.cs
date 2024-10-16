
using Datalayer.Domain.Group;
using Infrastructure.CQRS.Models;
using System;
using System.Linq.Expressions;

namespace Infrastructure.CQRS.Projections
{
    public class ProjectSearchDetails
    {
        public static Expression<Func<Project, ProjectSearchResult>> ProjectSearch
        {
            get =>
                o => new ProjectSearchResult
                {
                    Id = o.Id,
                    Name = o.Name,
                    Description = o.Description,
                    ImageUrl = o.ImageUrl,
                    //Group = o.Group,
                    GroupId  = o.GroupId,
                    //Tasks = o.Tasks,
                    //Series = o.Series,
                    //ProjectDocuments = o.ProjectDocuments,
                    State = o.State,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    ModifiedBy = o.ModifiedBy,
                    ModifiedDate = o.ModifiedDate,

                };
        }
    }

}


