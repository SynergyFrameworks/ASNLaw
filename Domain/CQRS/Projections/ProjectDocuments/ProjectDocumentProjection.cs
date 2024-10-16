using Datalayer.Domain.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Projections
{
    public class ProjectDocumentProjection
    {
        public static Expression<Func<ProjectDocument, ProjectDocument>> ProjectDocumentInformation
        {
            get =>
                o => new ProjectDocument
                {
                    Id = o.Id,
                    ProjectId = o.ProjectId,
                    Project = o.Project,
                    Url = o.Url,
                    Name = o.Name,
                    Description = o.Description,
                    Size = o.Size,
                    Extension = o.Extension,
                    IsOutput = o.IsOutput,
                    DocumentSourceId = o.DocumentSourceId,
                    DocumentSource = o.DocumentSource,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    ModifiedBy = o.ModifiedBy,
                    ModifiedDate = o.ModifiedDate,

                };
        }
    }
}



