using Microsoft.AspNetCore.Http;
using Scion.FilesService.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scion.FilesService.Services
{
    public class FilesService : IFilesService
    {
        private readonly IFileManager _fileManager;
        public FilesService(IResourceFactory resourceFactory)
        {
            _fileManager = resourceFactory.GetFileManager();
        }

        public Task<ScionStream> GetFile(string fileIdentifier)
        {
            return _fileManager.GetFileAsync(fileIdentifier);
        }
        public IEnumerable<File> GetFiles()
        {
            return _fileManager.GetFileInfos();
        }

        public Task<FileResult> UploadFile(IFormFile file)
        {
            using (var fileStream = file.OpenReadStream())
            {
                return _fileManager.UploadFileAsync(fileStream, new File { Name = file.FileName, ContentType = file.ContentType });
            }
        }

        public Task<FileResult> DeleteFile(string fileIdentifier)
        {
            return _fileManager.DeleteFileAsync(fileIdentifier);
        }

    }
}
