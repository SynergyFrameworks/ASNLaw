using Hangfire;
using Hangfire.Server;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using Scion.Hangfire;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.Exceptions;
using Scion.Infrastructure.ExportImport;
using Scion.Infrastructure.ExportImport.PushNotifications;
using Scion.Infrastructure.Extensions;
using Scion.Infrastructure.PushNotifications;
using Scion.Infrastructure.Security;
using Scion.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Permissions = Scion.Infrastructure.PlatformConstants.Security.Permissions;

namespace Scion.Infrastructure.Web.Controllers.Api
{
    [Route("api/platform")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public class PlatformExportImportController : Controller
    {
        private readonly IPlatformExportImportManager _platformExportManager;
        private readonly IPushNotificationManager _pushNotifier;
        private readonly ISettingsManager _settingsManager;
        private readonly IUserNameResolver _userNameResolver;
        private readonly PlatformOptions _platformOptions;

        private static readonly object _lockObject = new object();

        public PlatformExportImportController(
            //  IPlatformExportImportManager platformExportManager,
            IPushNotificationManager pushNotifier,
            ISettingsManager settingManager,
            IUserNameResolver userNameResolver,
            IOptions<PlatformOptions> options)
        {
            //   _platformExportManager = platformExportManager;
            _pushNotifier = pushNotifier;
            _settingsManager = settingManager;
            _userNameResolver = userNameResolver;
            _platformOptions = options.Value;
        }

        [HttpGet]
        [Route("sampledata/discover")]
        [AllowAnonymous]
        public ActionResult<SampleDataInfo[]> DiscoverSampleData()
        {
            return Ok(InnerDiscoverSampleData().ToArray());
        }

        [HttpPost]
        [Route("sampledata/autoinstall")]
        [Authorize(Permissions.PlatformImport)]
        public ActionResult<SampleDataImportPushNotification> TryToAutoInstallSampleData()
        {
            SampleDataInfo sampleData = InnerDiscoverSampleData().FirstOrDefault(x => !x.Url.IsNullOrEmpty());
            if (sampleData != null)
            {
                return ImportSampleData(sampleData.Url);
            }

            return Ok();
        }

        [HttpPost]
        [Route("sampledata/import")]
        [Authorize(Permissions.PlatformImport)]
        public ActionResult<SampleDataImportPushNotification> ImportSampleData([FromQuery] string url = null)
        {
            lock (_lockObject)
            {
                SampleDataState sampleDataState = EnumUtility.SafeParse(_settingsManager.GetValue(PlatformConstants.Settings.Setup.SampleDataState.Name, SampleDataState.Undefined.ToString()), SampleDataState.Undefined);
                if (sampleDataState == SampleDataState.Undefined && Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    _settingsManager.SetValue(PlatformConstants.Settings.Setup.SampleDataState.Name, SampleDataState.Processing);
                    SampleDataImportPushNotification pushNotification = new SampleDataImportPushNotification(User.Identity.Name);
                    _pushNotifier.Send(pushNotification);
                    string jobId = BackgroundJob.Enqueue(() => SampleDataImportBackgroundAsync(new Uri(url), pushNotification, JobCancellationToken.Null, null));
                    pushNotification.JobId = jobId;

                    return Ok(pushNotification);
                }
            }

            return Ok();
        }

        /// <summary>
        /// This method used for azure automatically deployment
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("sampledata/state")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [AllowAnonymous]
        public ActionResult<SampleDataState> GetSampleDataState()
        {
            SampleDataState state = EnumUtility.SafeParse(_settingsManager.GetValue(PlatformConstants.Settings.Setup.SampleDataState.Name, string.Empty), SampleDataState.Undefined);
            return Ok(state);
        }

        //[HttpGet]
        //[Route("export/manifest/new")]
        //public ActionResult<PlatformExportManifest> GetNewExportManifest()
        //{
        //    return Ok(_platformExportManager.GetNewExportManifest(_userNameResolver.GetCurrentUserName()));
        //}

        //[HttpGet]
        //[Route("export/manifest/load")]
        //public ActionResult<PlatformExportManifest> LoadExportManifest([FromQuery] string fileUrl)
        //{
        //    if (string.IsNullOrEmpty(fileUrl))
        //    {
        //        throw new ArgumentNullException(nameof(fileUrl));
        //    }

        //    var uploadFolderPath = Path.GetFullPath(_platformOptions.LocalUploadFolderPath);

        //    var localPath = Path.Combine(uploadFolderPath, fileUrl);
        //    if (!localPath.StartsWith(uploadFolderPath))
        //    {
        //        throw new PlatformException($"Invalid path {localPath}");
        //    }

        //    PlatformExportManifest retVal;
        //    using (var stream = new FileStream(localPath, FileMode.Open))
        //    {
        //        retVal = _platformExportManager.ReadExportManifest(stream);
        //    }
        //    return Ok(retVal);
        //}

        [HttpPost]
        [Route("export")]
        [Authorize(Permissions.PlatformImport)]
        public ActionResult<PlatformExportPushNotification> ProcessExport([FromBody] PlatformImportExportRequest exportRequest)
        {
            PlatformExportPushNotification notification = new PlatformExportPushNotification(_userNameResolver.GetCurrentUserName())
            {
                Title = "Platform export task",
                Description = "starting export...."
            };
            _pushNotifier.Send(notification);

            string jobId = BackgroundJob.Enqueue(() => PlatformExportBackgroundAsync(exportRequest, notification, JobCancellationToken.Null, null));
            notification.JobId = jobId;
            return Ok(notification);
        }

        [HttpPost]
        [Route("import")]
        [Authorize(Permissions.PlatformImport)]
        public ActionResult<PlatformImportPushNotification> ProcessImport([FromBody] PlatformImportExportRequest importRequest)
        {
            PlatformImportPushNotification notification = new PlatformImportPushNotification(_userNameResolver.GetCurrentUserName())
            {
                Title = "Platform import task",
                Description = "starting import...."
            };
            _pushNotifier.Send(notification);

            string jobId = BackgroundJob.Enqueue(() => PlatformImportBackgroundAsync(importRequest, notification, JobCancellationToken.Null, null));
            notification.JobId = jobId;

            return Ok(notification);
        }

        [HttpPost]
        [Route("exortimport/tasks/{jobId}/cancel")]
        public ActionResult Cancel([FromRoute] string jobId)
        {
            BackgroundJob.Delete(jobId);
            return Ok();
        }

        [HttpGet]
        [Route("export/download/{fileName}")]
        [Authorize(Permissions.PlatformExport)]
        public ActionResult DownloadExportFile([FromRoute] string fileName)
        {
            string localTmpFolder = Path.GetFullPath(Path.Combine(_platformOptions.DefaultExportFolder));
            string localPath = Path.Combine(localTmpFolder, Path.GetFileName(fileName));

            //Load source data only from local file system
            using (FileStream stream = System.IO.File.Open(localPath, FileMode.Open))
            {
                FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(localPath, out string contentType))
                {
                    contentType = "application/octet-stream";
                }
                return PhysicalFile(localPath, contentType);
            }
        }

        private IEnumerable<SampleDataInfo> InnerDiscoverSampleData()
        {
            string sampleDataUrl = _platformOptions.SampleDataUrl;
            if (string.IsNullOrEmpty(sampleDataUrl))
            {
                return Enumerable.Empty<SampleDataInfo>();
            }

            //Direct file mode
            if (sampleDataUrl.EndsWith(".zip"))
            {
                return new List<SampleDataInfo>
                {
                    new SampleDataInfo { Url = sampleDataUrl }
                };
            }

            //Discovery mode
            string manifestUrl = sampleDataUrl + "\\manifest.json";
            using (WebClient client = new WebClient())
            using (Stream stream = client.OpenRead(new Uri(manifestUrl)))
            {
                //Add empty template
                List<SampleDataInfo> result = new List<SampleDataInfo>
                {
                    new SampleDataInfo { Name = "Empty" }
                };

                //Need filter unsupported versions and take one most new sample data
                List<SampleDataInfo> sampleDataInfos = stream.DeserializeJson<List<SampleDataInfo>>()
                    .Select(x => new
                    {
                        Version = SemanticVersion.Parse(x.PlatformVersion),
                        x.Name,
                        Data = x
                    })
                    .Where(x => x.Version.IsCompatibleWith(PlatformVersion.CurrentVersion))
                    .GroupBy(x => x.Name)
                    .Select(x => x.OrderByDescending(y => y.Version).First().Data)
                    .ToList();

                //Convert relative  sample data urls to absolute
                foreach (SampleDataInfo sampleDataInfo in sampleDataInfos)
                {
                    if (!Uri.IsWellFormedUriString(sampleDataInfo.Url, UriKind.Absolute))
                    {
                        Uri uri = new Uri(sampleDataUrl);
                        sampleDataInfo.Url = new Uri(uri, uri.AbsolutePath + "/" + sampleDataInfo.Url).ToString();
                    }
                }

                result.AddRange(sampleDataInfos);

                return result;
            }
        }

        public async Task SampleDataImportBackgroundAsync(Uri url, SampleDataImportPushNotification pushNotification, IJobCancellationToken cancellationToken, PerformContext context)
        {
            void progressCallback(ExportImportProgressInfo x)
            {
                pushNotification.Path(x);
                pushNotification.JobId = context.BackgroundJob.Id;
                _pushNotifier.Send(pushNotification);
            }

            try
            {
                pushNotification.Description = "Start downloading from " + url;

                await _pushNotifier.SendAsync(pushNotification);

                string tmpPath = Path.GetFullPath(_platformOptions.LocalUploadFolderPath);
                if (!Directory.Exists(tmpPath))
                {
                    Directory.CreateDirectory(tmpPath);
                }

                string tmpFilePath = Path.Combine(tmpPath, Path.GetFileName(url.ToString()));
                using (WebClient client = new WebClient())
                {
                    client.DownloadProgressChanged += async (sender, args) =>
                    {
                        pushNotification.Description = string.Format("Sample data {0} of {1} downloading...", args.BytesReceived.ToHumanReadableSize(), args.TotalBytesToReceive.ToHumanReadableSize());
                        await _pushNotifier.SendAsync(pushNotification);
                    };
                    Task task = client.DownloadFileTaskAsync(url, tmpFilePath);
                    task.Wait();
                }
                using (FileStream stream = new FileStream(tmpFilePath, FileMode.Open))
                {
                    PlatformExportManifest manifest = _platformExportManager.ReadExportManifest(stream);
                    if (manifest != null)
                    {
                        await _platformExportManager.ImportAsync(stream, manifest, progressCallback, new JobCancellationTokenWrapper(cancellationToken));
                    }
                }
            }
            catch (JobAbortedException)
            {
                //do nothing
            }
            catch (Exception ex)
            {
                pushNotification.Errors.Add(ex.ExpandExceptionMessage());
            }
            finally
            {
                _settingsManager.SetValue(PlatformConstants.Settings.Setup.SampleDataState.Name, SampleDataState.Completed);
                pushNotification.Description = "Import finished";
                pushNotification.Finished = DateTime.UtcNow;
                await _pushNotifier.SendAsync(pushNotification);
            }
        }

        public async Task PlatformImportBackgroundAsync(PlatformImportExportRequest importRequest, PlatformImportPushNotification pushNotification, IJobCancellationToken cancellationToken, PerformContext context)
        {
            void progressCallback(ExportImportProgressInfo x)
            {
                pushNotification.Path(x);
                pushNotification.JobId = context.BackgroundJob.Id;
                _pushNotifier.Send(pushNotification);
            }

            DateTime now = DateTime.UtcNow;
            try
            {
                JobCancellationTokenWrapper cancellationTokenWrapper = new JobCancellationTokenWrapper(cancellationToken);

                string uploadFolderFullPath = Path.GetFullPath(_platformOptions.LocalUploadFolderPath);
                // VP-5353: Checking that the file is inside LocalUploadFolderPath
                string localPath = Path.Combine(uploadFolderFullPath, importRequest.FileUrl);

                if (!localPath.StartsWith(uploadFolderFullPath))
                {
                    throw new PlatformException($"Invalid path {localPath}");
                }

                //Load source data only from local file system
                using (FileStream stream = new FileStream(localPath, FileMode.Open))
                {
                    PlatformExportManifest manifest = importRequest.ToManifest();
                    manifest.Created = now;
                    await _platformExportManager.ImportAsync(stream, manifest, progressCallback, cancellationTokenWrapper);
                }
            }
            catch (JobAbortedException)
            {
                //do nothing
            }
            catch (Exception ex)
            {
                pushNotification.Errors.Add(ex.ExpandExceptionMessage());
            }
            finally
            {
                pushNotification.Description = "Import finished";
                pushNotification.Finished = DateTime.UtcNow;
                await _pushNotifier.SendAsync(pushNotification);
            }
        }

        public async Task PlatformExportBackgroundAsync(PlatformImportExportRequest exportRequest, PlatformExportPushNotification pushNotification, IJobCancellationToken cancellationToken, PerformContext context)
        {
            void progressCallback(ExportImportProgressInfo x)
            {
                pushNotification.Path(x);
                pushNotification.JobId = context.BackgroundJob.Id;
                _pushNotifier.Send(pushNotification);
            }

            try
            {
                string fileName = string.Format(_platformOptions.DefaultExportFileName, DateTime.UtcNow);
                string localTmpFolder = Path.GetFullPath(Path.Combine(_platformOptions.DefaultExportFolder));
                string localTmpPath = Path.Combine(localTmpFolder, Path.GetFileName(fileName));

                if (!Directory.Exists(localTmpFolder))
                {
                    Directory.CreateDirectory(localTmpFolder);
                }

                if (System.IO.File.Exists(localTmpPath))
                {
                    System.IO.File.Delete(localTmpPath);
                }

                //Import first to local tmp folder because Azure blob storage doesn't support some special file access mode
                using (FileStream stream = System.IO.File.OpenWrite(localTmpPath))
                {
                    PlatformExportManifest manifest = exportRequest.ToManifest();
                    await _platformExportManager.ExportAsync(stream, manifest, progressCallback, new JobCancellationTokenWrapper(cancellationToken));
                    pushNotification.DownloadUrl = $"api/platform/export/download/{fileName}";
                }
            }
            catch (JobAbortedException)
            {
                //do nothing
            }
            catch (Exception ex)
            {
                pushNotification.Errors.Add(ex.ExpandExceptionMessage());
            }
            finally
            {
                pushNotification.Description = "Export finished";
                pushNotification.Finished = DateTime.UtcNow;
                await _pushNotifier.SendAsync(pushNotification);
            }
        }
    }
}
