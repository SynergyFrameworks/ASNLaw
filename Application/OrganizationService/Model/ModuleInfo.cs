using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationService.Model
{
    public class ModuleInfo
    {
        public Guid Id { get; set; }

        public string Version { get; set; }

        public string VersionTag { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string LicenseUrl { get; set; }

        public string ProjectUrl { get; set; }

        public bool RequireLicenseAcceptance { get; set; }

        public string Notes { get; set; }

        public string Tags { get; set; }

        public bool IsRemovable { get; set; }

        public bool IsInstalled { get; set; }

        public string Authors { get; set; }

        public string Owners { get; set; }

    }
}
