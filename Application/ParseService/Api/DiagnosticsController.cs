// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scion.Infrastructure.Core.Common;
using Scion.Infrastructure.Core.Modularity;
using Scion.Infrastructure.Licensing;
using Scion.Infrastructure.Model.Diagnostics;
using Scion.Infrastructure.Modularity;

namespace Scion.Infrastructure.Controllers.Api
{
    [Route("api/platform/diagnostics")]
    [Authorize]
    public class DiagnosticsController : Controller
    {
        private readonly IModuleCatalog _moduleCatalog;
        private readonly LicenseProvider _licenseProvider;

        public DiagnosticsController(IModuleCatalog moduleCatalog, LicenseProvider licenseProvider)
        {
            _moduleCatalog = moduleCatalog;
            _licenseProvider = licenseProvider;

        }

        [HttpGet]
        [Route("systeminfo")]
        public ActionResult<SystemInfo> GetSystemInfo()
        {
            var platformVersion = PlatformVersion.CurrentVersion.ToString();
            var license = _licenseProvider.GetLicense();

            var installedModules = _moduleCatalog.Modules.OfType<ManifestModuleInfo>().Where(x => x.IsInstalled).OrderBy(x => x.Id)
                                       .Select(x => new ModuleDescriptor(x))
                                       .ToArray();

            var result = new SystemInfo()
            {
                PlatformVersion = platformVersion,
                License = license,
                InstalledModules = installedModules,
                Version = Environment.Version.ToString(),
                Is64BitOperatingSystem = Environment.Is64BitOperatingSystem,
                Is64BitProcess = Environment.Is64BitProcess,
            };

            return Ok(result);
        }
    }
}
