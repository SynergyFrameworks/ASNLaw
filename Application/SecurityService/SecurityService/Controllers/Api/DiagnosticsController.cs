// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.Extensions;
using Scion.Infrastructure.Licensing;
using Scion.Infrastructure.Model.Diagnostics;
using Scion.Infrastructure.Modularity;
using Scion.Infrastructure.Web.Licensing;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace Scion.Infrastructure.Web.Controllers.Api
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
        public async Task<ActionResult<SystemInfo>> GetSystemInfo()
        {
            string platformVersion = PlatformVersion.CurrentVersion.ToString();
            License license = await _licenseProvider.GetLicenseAsync();

            ModuleDescriptor[] installedModules = _moduleCatalog.Modules.OfType<ManifestModuleInfo>().Where(x => x.IsInstalled).OrderBy(x => x.Id)
                                       .Select(x => new ModuleDescriptor(x))
                                       .ToArray();

            SystemInfo result = new SystemInfo()
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

        /// <summary>
        /// Get installed modules with errors
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("errors")]
        [AllowAnonymous]
        public ActionResult<ModuleDescriptor[]> GetModulesErrors()
        {

            ModuleDescriptor[] result = _moduleCatalog.Modules.OfType<ManifestModuleInfo>()
                .Where(x => !x.Errors.IsNullOrEmpty())
                .OrderBy(x => x.Id)
                .ThenBy(x => x.Version)
                .Select(x => new ModuleDescriptor(x))
                .ToArray();

            return Ok(result);
        }
    }
}
