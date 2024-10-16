using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Scion.Data.Helpers;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.Extensions;
using Scion.Infrastructure.Modularity;
using Scion.Infrastructure.Modularity.PushNotifications;
using Scion.Infrastructure.PushNotifications;
using Scion.Infrastructure.Security;
using Scion.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Scion.Infrastructure.Web.Controllers.Api
{
    [Route("api/platform/modules")]
    [Authorize]
    public class ModulesController : Controller
    {
        private readonly IExternalModuleCatalog _externalModuleCatalog;
        private readonly IModuleInstaller _moduleInstaller;
        private readonly IPushNotificationManager _pushNotifier;
        private readonly IUserNameResolver _userNameResolver;
        private readonly ISettingsManager _settingsManager;
        private readonly PlatformOptions _platformOptions;
        private readonly ExternalModuleCatalogOptions _externalModuleCatalogOptions;
        private readonly IPlatformRestarter _platformRestarter;
        private static readonly object _lockObject = new object();
        private static readonly FormOptions _defaultFormOptions = new FormOptions();

        public ModulesController(IExternalModuleCatalog externalModuleCatalog, IModuleInstaller moduleInstaller, IPushNotificationManager pushNotifier, IUserNameResolver userNameResolver, ISettingsManager settingsManager, IOptions<PlatformOptions> platformOptions, IOptions<ExternalModuleCatalogOptions> externalModuleCatalogOptions, IPlatformRestarter platformRestarter)
        {
            _externalModuleCatalog = externalModuleCatalog;
            _moduleInstaller = moduleInstaller;
            _pushNotifier = pushNotifier;
            _userNameResolver = userNameResolver;
            _settingsManager = settingsManager;
            _platformOptions = platformOptions.Value;
            _externalModuleCatalogOptions = externalModuleCatalogOptions.Value;
            _platformRestarter = platformRestarter;
        }

        /// <summary>
        /// Reload  modules
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("reload")]
        [Authorize(PlatformConstants.Security.Permissions.ModuleQuery)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        public ActionResult ReloadModules()
        {
            _externalModuleCatalog.Reload();
            return NoContent();
        }

        /// <summary>
        /// Get installed modules
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Authorize(PlatformConstants.Security.Permissions.ModuleQuery)]
        public ActionResult<ModuleDescriptor[]> GetModules()
        {
            EnsureModulesCatalogInitialized();

            ModuleDescriptor[] retVal = _externalModuleCatalog.Modules.OfType<ManifestModuleInfo>().OrderBy(x => x.Id).ThenBy(x => x.Version)
                                               .Select(x => new ModuleDescriptor(x))
                                               .ToArray();

            return Ok(retVal);
        }

        /// <summary>
        /// Get all dependent modules for module
        /// </summary>
        /// <param name="moduleDescriptors">modules descriptors</param>
        /// <returns></returns>
        [HttpPost]
        [Route("getdependents")]
        [Authorize(PlatformConstants.Security.Permissions.ModuleQuery)]
        public ActionResult<ModuleDescriptor[]> GetDependingModules([FromBody] ModuleDescriptor[] moduleDescriptors)
        {
            EnsureModulesCatalogInitialized();

            List<ManifestModuleInfo> modules = _externalModuleCatalog.Modules
                .OfType<ManifestModuleInfo>()
                .Join(moduleDescriptors, x => x.Identity, y => y.Identity, (x, y) => x)
                .ToList();

            ModuleDescriptor[] retVal = GetDependingModulesRecursive(modules).Distinct()
                                                              .Except(modules)
                                                              .Select(x => new ModuleDescriptor(x))
                                                              .ToArray();
            return Ok(retVal);
        }

        /// <summary>
        /// Returns a flat expanded  list of modules that depend on passed modules
        /// </summary>
        /// <param name="moduleDescriptors">modules descriptors</param>
        /// <returns></returns>
        [HttpPost]
        [Route("getmissingdependencies")]
        [Authorize(PlatformConstants.Security.Permissions.ModuleQuery)]
        public ActionResult<ModuleDescriptor[]> GetMissingDependencies([FromBody] ModuleDescriptor[] moduleDescriptors)
        {
            EnsureModulesCatalogInitialized();
            List<ManifestModuleInfo> modules = _externalModuleCatalog.Modules
                                        .OfType<ManifestModuleInfo>().Join(moduleDescriptors, x => x.Identity, y => y.Identity, (x, y) => x)
                                        .ToList();

            ModuleDescriptor[] result = _externalModuleCatalog.CompleteListWithDependencies(modules)
                                       .OfType<ManifestModuleInfo>()
                                       .Where(x => !x.IsInstalled)
                                       .Except(modules)
                                       .Select(x => new ModuleDescriptor(x))
                                       .ToArray();

            return Ok(result);
        }

        /// <summary>
        /// Upload module package for installation or update
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("localstorage")]
        [Authorize(PlatformConstants.Security.Permissions.ModuleManage)]
        public async Task<ActionResult<ModuleDescriptor>> UploadModuleArchive()
        {
            EnsureModulesCatalogInitialized();
            ModuleDescriptor result = null;
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
            }
            string uploadPath = Path.GetFullPath(_platformOptions.LocalUploadFolderPath);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            string targetFilePath = null;

            string boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType), _defaultFormOptions.MultipartBoundaryLengthLimit);
            MultipartReader reader = new MultipartReader(boundary, HttpContext.Request.Body);

            MultipartSection section = await reader.ReadNextSectionAsync();
            if (section != null)
            {
                bool hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out ContentDispositionHeaderValue contentDisposition);

                if (hasContentDispositionHeader)
                {
                    if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {
                        string fileName = contentDisposition.FileName.Value;
                        targetFilePath = Path.Combine(uploadPath, fileName);

                        using (FileStream targetStream = System.IO.File.Create(targetFilePath))
                        {
                            await section.Body.CopyToAsync(targetStream);
                        }

                    }
                }
                using (FileStream packageStream = System.IO.File.Open(targetFilePath, FileMode.Open))
                using (ZipArchive package = new ZipArchive(packageStream, ZipArchiveMode.Read))
                {
                    ZipArchiveEntry entry = package.GetEntry("module.manifest");
                    if (entry != null)
                    {
                        using (Stream manifestStream = entry.Open())
                        {
                            ModuleManifest manifest = ManifestReader.Read(manifestStream);
                            ManifestModuleInfo module = AbstractTypeFactory<ManifestModuleInfo>.TryCreateInstance();
                            module.LoadFromManifest(manifest);
                            ManifestModuleInfo alreadyExistModule = _externalModuleCatalog.Modules.OfType<ManifestModuleInfo>().FirstOrDefault(x => x.Equals(module));
                            if (alreadyExistModule != null)
                            {
                                module = alreadyExistModule;
                            }
                            else
                            {
                                //Force dependency validation for new module
                                _externalModuleCatalog.CompleteListWithDependencies(new[] { module }).ToList().Clear();
                                _externalModuleCatalog.AddModule(module);
                            }
                            module.Ref = targetFilePath;
                            result = new ModuleDescriptor(module);
                        }
                    }
                }
            }
            return Ok(result);
        }

        /// <summary>
        /// Install modules 
        /// </summary>
        /// <param name="modules">modules for install</param>
        /// <returns></returns>
        [HttpPost]
        [Route("install")]
        [Authorize(PlatformConstants.Security.Permissions.ModuleManage)]
        public ActionResult<ModulePushNotification> InstallModules([FromBody] ModuleDescriptor[] modules)
        {
            EnsureModulesCatalogInitialized();

            ModuleBackgroundJobOptions options = new ModuleBackgroundJobOptions
            {
                Action = ModuleAction.Install,
                Modules = modules
            };
            ModulePushNotification result = ScheduleJob(options);
            return Ok(result);
        }

        /// <summary>
        /// Uninstall module
        /// </summary>
        /// <param name="modules">modules</param>
        /// <returns></returns>
        [HttpPost]
        [Route("uninstall")]
        [Authorize(PlatformConstants.Security.Permissions.ModuleManage)]
        public ActionResult<ModulePushNotification> UninstallModule([FromBody] ModuleDescriptor[] modules)
        {
            EnsureModulesCatalogInitialized();

            ModuleBackgroundJobOptions options = new ModuleBackgroundJobOptions
            {
                Action = ModuleAction.Uninstall,
                Modules = modules
            };
            ModulePushNotification result = ScheduleJob(options);
            return Ok(result);
        }

        /// <summary>
        /// Restart web application
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("restart")]
        [Authorize(PlatformConstants.Security.Permissions.ModuleManage)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        public ActionResult Restart()
        {
            _platformRestarter.Restart();

            return NoContent();
        }

        /// <summary>
        /// Auto-install modules with specified groups
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("autoinstall")]
        public ActionResult<ModuleAutoInstallPushNotification> TryToAutoInstallModules()
        {
            ModuleAutoInstallPushNotification notification = new ModuleAutoInstallPushNotification(User.Identity.Name)
            {
                Title = "Modules installation",
                //set completed by default
                Finished = DateTime.UtcNow
            };

            if (!_settingsManager.GetValue(PlatformConstants.Settings.Setup.ModulesAutoInstalled.Name, false))
            {
                lock (_lockObject)
                {
                    if (!_settingsManager.GetValue(PlatformConstants.Settings.Setup.ModulesAutoInstalled.Name, false))
                    {
                        string[] moduleBundles = _externalModuleCatalogOptions.AutoInstallModuleBundles;
                        if (!moduleBundles.IsNullOrEmpty())
                        {
                            _settingsManager.SetValue(PlatformConstants.Settings.Setup.ModulesAutoInstalled.Name, true);
                            _settingsManager.SetValue(PlatformConstants.Settings.Setup.ModulesAutoInstallState.Name, AutoInstallState.Processing);

                            EnsureModulesCatalogInitialized();

                            List<ManifestModuleInfo> modules = new List<ManifestModuleInfo>();
                            IEnumerable<IGrouping<string, ManifestModuleInfo>> moduleVersionGroups = _externalModuleCatalog.Modules
                                .OfType<ManifestModuleInfo>()
                                .Where(x => x.Groups.Intersect(moduleBundles, StringComparer.OrdinalIgnoreCase).Any())
                                .GroupBy(x => x.Id);

                            //Need install only latest versions
                            foreach (IGrouping<string, ManifestModuleInfo> moduleVersionGroup in moduleVersionGroups)
                            {
                                ManifestModuleInfo alreadyInstalledModule = _externalModuleCatalog.Modules.OfType<ManifestModuleInfo>().FirstOrDefault(x => x.IsInstalled && x.Id.EqualsInvariant(moduleVersionGroup.Key));
                                //skip already installed modules
                                if (alreadyInstalledModule == null)
                                {
                                    ManifestModuleInfo latestVersion = moduleVersionGroup.OrderBy(x => x.Version).LastOrDefault();
                                    if (latestVersion != null)
                                    {
                                        modules.Add(latestVersion);
                                    }
                                }
                            }

                            ModuleDescriptor[] modulesWithDependencies = _externalModuleCatalog.CompleteListWithDependencies(modules)
                                .OfType<ManifestModuleInfo>()
                                .Where(x => !x.IsInstalled)
                                .Select(x => new ModuleDescriptor(x))
                                .ToArray();

                            if (modulesWithDependencies.Any())
                            {
                                ModuleBackgroundJobOptions options = new ModuleBackgroundJobOptions
                                {
                                    Action = ModuleAction.Install,
                                    Modules = modulesWithDependencies
                                };
                                //reset finished date
                                notification.Finished = null;

                                // can't use Hangfire.BackgroundJob.Enqueue(...), because Hangfire tables might be missing in new DB
                                new Thread(() =>
                                {
                                    Thread.CurrentThread.IsBackground = true;
                                    ModuleBackgroundJob(options, notification);
                                }).Start();
                            }
                        }
                    }
                }
            }
            return Ok(notification);
        }

        /// <summary>
        /// This method used by azure automatically deployment scripts to check the installation status
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("autoinstall/state")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [AllowAnonymous]
        public ActionResult<string> GetAutoInstallState()
        {
            AutoInstallState state = EnumUtility.SafeParse(_settingsManager.GetValue(PlatformConstants.Settings.Setup.ModulesAutoInstallState.Name, string.Empty), AutoInstallState.Undefined);
            return Ok(state);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public void ModuleBackgroundJob(ModuleBackgroundJobOptions options, ModulePushNotification notification)
        {
            try
            {
                notification.Started = DateTime.UtcNow;
                ManifestModuleInfo[] moduleInfos = _externalModuleCatalog.Modules.OfType<ManifestModuleInfo>()
                                     .Where(x => options.Modules.Any(y => y.Identity.Equals(x.Identity)))
                                     .ToArray();
                Progress<ProgressMessage> reportProgress = new Progress<ProgressMessage>(m =>
                {
                    lock (_lockObject)
                    {
                        notification.Description = m.Message;
                        notification.ProgressLog.Add(m);
                        _pushNotifier.Send(notification);
                    }
                });

                switch (options.Action)
                {
                    case ModuleAction.Install:
                        _moduleInstaller.Install(moduleInfos, reportProgress);
                        break;
                    case ModuleAction.Uninstall:
                        _moduleInstaller.Uninstall(moduleInfos, reportProgress);
                        break;
                }
            }
            catch (Exception ex)
            {
                notification.ProgressLog.Add(new ProgressMessage
                {
                    Level = ProgressMessageLevel.Error,
                    Message = ex.ToString(),
                });
            }
            finally
            {
                _settingsManager.SetValue(PlatformConstants.Settings.Setup.ModulesAutoInstallState.Name, AutoInstallState.Completed);

                notification.Finished = DateTime.UtcNow;
                notification.Description = "Installation finished.";
                notification.ProgressLog.Add(new ProgressMessage
                {
                    Level = ProgressMessageLevel.Info,
                    Message = notification.Description,
                });
                _pushNotifier.Send(notification);
            }
        }

        private void EnsureModulesCatalogInitialized()
        {
            _externalModuleCatalog.Initialize();
        }

        private IEnumerable<ManifestModuleInfo> GetDependingModulesRecursive(IEnumerable<ManifestModuleInfo> modules)
        {
            List<ManifestModuleInfo> retVal = new List<ManifestModuleInfo>();
            foreach (ManifestModuleInfo module in modules)
            {
                retVal.Add(module);
                List<ManifestModuleInfo> dependingModules = _externalModuleCatalog.Modules.OfType<ManifestModuleInfo>()
                                                             .Where(x => x.IsInstalled)
                                                             .Where(x => x.DependsOn.Contains(module.Id, StringComparer.OrdinalIgnoreCase))
                                                             .ToList();
                if (dependingModules.Any())
                {
                    retVal.AddRange(GetDependingModulesRecursive(dependingModules));
                }
            }
            return retVal;
        }

        private ModulePushNotification ScheduleJob(ModuleBackgroundJobOptions options)
        {
            ModulePushNotification notification = new ModulePushNotification(_userNameResolver.GetCurrentUserName());

            switch (options.Action)
            {
                case ModuleAction.Install:
                    notification.Title = "Install Module";
                    notification.ProgressLog.Add(new ProgressMessage { Level = ProgressMessageLevel.Info, Message = "Starting installation..." });
                    break;
                case ModuleAction.Uninstall:
                    notification.Title = "Uninstall Module";
                    notification.ProgressLog.Add(new ProgressMessage { Level = ProgressMessageLevel.Info, Message = "Starting uninstall..." });
                    break;
            }

            _pushNotifier.Send(notification);

            BackgroundJob.Enqueue(() => ModuleBackgroundJob(options, notification));

            return notification;
        }
    }
}
