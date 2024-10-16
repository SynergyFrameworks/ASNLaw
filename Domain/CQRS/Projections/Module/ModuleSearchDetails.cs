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
    public class ModuleSearchDetails
    {
        public static Expression<Func<Module, ModuleSearchResult>> ModuleSearch
        {
            get =>
                o => new ModuleSearchResult
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
                    IsRemovable = o.IsRemovable,
                    Authors = o.Authors,
                    Owners=o.Owners,
                    Groups = o.Groups,
                    Resources = o.Resources,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    ModifiedBy = o.ModifiedBy,
                    ModifiedDate = o.ModifiedDate,

                };
        }
    }
}
