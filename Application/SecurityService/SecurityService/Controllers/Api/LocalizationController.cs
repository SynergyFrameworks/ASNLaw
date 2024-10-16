using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Scion.Infrastructure.Localizations;
using Scion.Infrastructure.Web.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Scion.Infrastructure.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/platform/localization")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class LocalizationController : Controller
    {
        private const string InternationalizationFilesFormat = ".js";
        private static readonly string InternationalizationFilesFolder = $"js{Path.DirectorySeparatorChar}i18n{Path.DirectorySeparatorChar}angular";

        private readonly IWebHostEnvironment _hostingEnv;
        private readonly ITranslationService _translationService;

        public LocalizationController(IWebHostEnvironment hostingEnv, ITranslationService translationService)
        {
            _hostingEnv = hostingEnv;
            _translationService = translationService;
        }

        /// <summary>
        /// Return localization resource
        /// </summary>
        /// <param name="lang">Language of localization resource (en by default)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public ActionResult<JObject> GetLocalization(string lang = null)
        {
            JObject result = _translationService.GetTranslationDataForLanguage(lang);

            return Ok(result);
        }

        /// <summary>
        /// Return all available locales
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("locales")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [AllowAnonymous]
        public ActionResult<string[]> GetLocales()
        {
            string[] locales = _translationService.GetListOfInstalledLanguages();

            return Ok(locales);
        }

        /// <summary>
        /// Return all available regional formats
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("regionalformats")]
        [AllowAnonymous]
        public ActionResult<string[]> GetRegionalFormats()
        {
            string[] files = GetAllInternationalizationFiles("*" + InternationalizationFilesFormat, InternationalizationFilesFolder);
            string[] formats = files
                .Select(Path.GetFileName)
                .Select(x =>
                {
                    int startIndexOfCode = x.IndexOf("_") + 1;
                    int endIndexOfCode = x.IndexOf(".");
                    return x.Substring(startIndexOfCode, endIndexOfCode - startIndexOfCode);
                }).Distinct().ToArray();

            return Ok(formats);
        }


        private string[] GetAllInternationalizationFiles(string searchPattern, string internationalizationsFolder)
        {
            List<string> files = new List<string>();

            // Get platform internationalization files
            string platformPath = _hostingEnv.MapPath("~/");
            string[] platformFileNames = GetFilesByPath(platformPath, searchPattern, internationalizationsFolder);
            files.AddRange(platformFileNames);

            return files.ToArray();
        }

        private string[] GetFilesByPath(string path, string searchPattern, string subfolder)
        {
            string sourceDirectoryPath = Path.Combine(path, subfolder);

            return Directory.Exists(sourceDirectoryPath)
                ? Directory.EnumerateFiles(sourceDirectoryPath, searchPattern, SearchOption.AllDirectories).ToArray()
                : new string[0];
        }
    }
}
