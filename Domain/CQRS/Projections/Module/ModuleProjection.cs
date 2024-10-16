using Datalayer.Domain.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.Projections
{
    public class ModuleProjection
    {
        public static Expression<Func<Module, Module>> ModuleInformation
        {
            get =>
                o => new Module
                {
                    Id = o.Id,
                    Version = o.Version,
                    VersionTag = o.VersionTag,
                    Title = o.Title,
                    Description = o.Description,
                    LicenseUrl = o.LicenseUrl,
                    ProjectUrl = o.ProjectUrl,
                    RequireLicenseAcceptance = o.RequireLicenseAcceptance,
                    Notes = o.Notes,
                    Tags = o.Tags,
                    IsInstalled = o.IsInstalled,
                    IsRemovable = o.IsRemovable
                };
        }
    }
}
