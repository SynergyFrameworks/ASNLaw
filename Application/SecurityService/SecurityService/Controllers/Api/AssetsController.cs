using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Scion.Infrastructure.Assets;
using Scion.Infrastructure.Common;
using Scion.Infrastructure.Extensions;
using Scion.Infrastructure.Web.Helpers;
using Scion.Infrastructure.Web.Swagger;
using Scion.Infrastructure.Web.Validators;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace Scion.Infrastructure.Web.Controllers.Api
{
    [Route("api/platform/assets")]
    public class AssetsController : Controller
    {
        private readonly IBlobStorageProvider _blobProvider;
        private readonly IBlobUrlResolver _urlResolver;
        private static readonly FormOptions _defaultFormOptions = new FormOptions();
        private readonly PlatformOptions _platformOptions;

        public AssetsController(IBlobStorageProvider blobProvider, IBlobUrlResolver urlResolver, IOptions<PlatformOptions> platformOptions)
        {
            _blobProvider = blobProvider;
            _urlResolver = urlResolver;
            _platformOptions = platformOptions.Value;
        }

        /// <summary>
        /// This method used to upload files on local disk storage in special uploads folder
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("localstorage")]
        [DisableFormValueModelBinding]
        [DisableRequestSizeLimit]
        [Authorize(PlatformConstants.Security.Permissions.AssetCreate)]
        public async Task<ActionResult<BlobInfo[]>> UploadAssetToLocalFileSystemAsync()
        {
            //ToDo Now supports downloading one file, find a solution for downloading multiple files
            // https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-3.1
            List<BlobInfo> result = new List<BlobInfo>();

            if (!Data.Helpers.MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
            }
            string uploadPath = Path.GetFullPath(_platformOptions.LocalUploadFolderPath);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string boundary = Data.Helpers.MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType), _defaultFormOptions.MultipartBoundaryLengthLimit);
            MultipartReader reader = new MultipartReader(boundary, HttpContext.Request.Body);

            MultipartSection section = await reader.ReadNextSectionAsync();
            if (section != null)
            {
                bool hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out ContentDispositionHeaderValue contentDisposition);

                if (hasContentDispositionHeader)
                {
                    if (Data.Helpers.MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {
                        string fileName = contentDisposition.FileName.Value;
                        string targetFilePath = Path.Combine(uploadPath, fileName);

                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }

                        using (FileStream targetStream = System.IO.File.Create(targetFilePath))
                        {
                            await section.Body.CopyToAsync(targetStream);
                        }

                        BlobInfo blobInfo = AbstractTypeFactory<BlobInfo>.TryCreateInstance();
                        blobInfo.Name = fileName;
                        //Use only file name as Url, for further access to these files need use PlatformOptions.LocalUploadFolderPath
                        blobInfo.Url = fileName;
                        blobInfo.ContentType = MimeTypeResolver.ResolveContentType(fileName);
                        result.Add(blobInfo);
                    }
                }
            }
            return Ok(result.ToArray());
        }

        /// <summary>
        /// Upload assets to the folder
        /// </summary>
        /// <remarks>
        /// Request body can contain multiple files.
        /// </remarks>
        /// <param name="folderUrl">Parent folder url (relative or absolute).</param>
        /// <param name="url">Url for uploaded remote resource (optional)</param>
        /// <param name="name">File name</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [DisableFormValueModelBinding]
        [Authorize(PlatformConstants.Security.Permissions.AssetCreate)]
        [UploadFile]
        public async Task<ActionResult<BlobInfo[]>> UploadAssetAsync([FromQuery] string folderUrl, [FromQuery] string url = null, [FromQuery] string name = null)
        {
            // https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-3.1
            if (url == null && !Data.Helpers.MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
            }

            List<BlobInfo> result = new List<BlobInfo>();
            if (url != null)
            {
                string fileName = name ?? HttpUtility.UrlDecode(Path.GetFileName(url));
                string fileUrl = folderUrl + "/" + fileName;
                using (WebClient client = new WebClient())
                using (Stream blobStream = _blobProvider.OpenWrite(fileUrl))
                using (Stream remoteStream = client.OpenRead(url))
                {
                    remoteStream.CopyTo(blobStream);
                    BlobInfo blobInfo = AbstractTypeFactory<BlobInfo>.TryCreateInstance();
                    blobInfo.Name = fileName;
                    blobInfo.RelativeUrl = fileUrl;
                    blobInfo.Url = _urlResolver.GetAbsoluteUrl(fileUrl);
                    result.Add(blobInfo);
                }
            }
            else
            {
                string boundary = Data.Helpers.MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType), _defaultFormOptions.MultipartBoundaryLengthLimit);
                MultipartReader reader = new MultipartReader(boundary, HttpContext.Request.Body);

                MultipartSection section = await reader.ReadNextSectionAsync();
                if (section != null)
                {
                    bool hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out ContentDispositionHeaderValue contentDisposition);

                    if (hasContentDispositionHeader)
                    {
                        if (Data.Helpers.MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                        {
                            string fileName = contentDisposition.FileName.Value;

                            string targetFilePath = folderUrl + "/" + fileName;

                            using (Stream targetStream = _blobProvider.OpenWrite(targetFilePath))
                            {
                                await section.Body.CopyToAsync(targetStream);
                            }

                            BlobInfo blobInfo = AbstractTypeFactory<BlobInfo>.TryCreateInstance();
                            blobInfo.Name = fileName;
                            blobInfo.RelativeUrl = targetFilePath;
                            blobInfo.Url = _urlResolver.GetAbsoluteUrl(targetFilePath);
                            blobInfo.ContentType = MimeTypeResolver.ResolveContentType(fileName);
                            result.Add(blobInfo);
                        }
                    }
                }
            }

            return Ok(result.ToArray());
        }

        /// <summary>
        /// Delete blobs by urls
        /// </summary>
        /// <param name="urls"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [Authorize(PlatformConstants.Security.Permissions.AssetDelete)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteBlobsAsync([FromQuery] string[] urls)
        {
            if (urls.IsNullOrEmpty())
                return BadRequest("Please, specify at least one asset URL to delete.");

            await _blobProvider.RemoveAsync(urls);
            return NoContent();
        }

        /// <summary>
        /// SearchAsync asset folders and blobs
        /// </summary>
        /// <param name="folderUrl"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Authorize(PlatformConstants.Security.Permissions.AssetRead)]
        public async Task<ActionResult<BlobEntrySearchResult>> SearchAssetItemsAsync([FromQuery] string folderUrl = null, [FromQuery] string keyword = null)
        {
            BlobEntrySearchResult result = await _blobProvider.SearchAsync(folderUrl, keyword);
            return Ok(result);
        }

        /// <summary>
        /// Create new blob folder
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("folder")]
        [Authorize(PlatformConstants.Security.Permissions.AssetCreate)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateBlobFolderAsync([FromBody] BlobFolder folder)
        {
            FluentValidation.Results.ValidationResult validation = new BlobFolderValidator().Validate(folder);

            if (!validation.IsValid)
            {
                return BadRequest(new
                {
                    Message = string.Join(" ", validation.Errors.Select(x => x.ErrorMessage)),
                    Errors = validation.Errors
                });
            }

            await _blobProvider.CreateFolderAsync(folder);
            return NoContent();
        }
    }
}
