using System;

namespace Infrastructure.Modularity
{
    public class ExternalModuleCatalogOptions
    {
        public Uri ModulesManifestUrl { get; set; }
        public string AuthorizationToken { get; set; }
        public string[] AutoInstallModuleBundles { get; set; } = new[] { "Scion Analytics" };
        public bool IncludePrerelease { get; set; }
    }
}
