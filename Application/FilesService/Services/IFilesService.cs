using Microsoft.AspNetCore.Http;
using Scion.FilesService.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scion.FilesService.Services
{
    public interface IFilesService
    {
        Task<FileResult> DeleteFile(string fileIdentifier);
        Task<ScionStream> GetFile(string fileIdentifier);
        IEnumerable<File> GetFiles();
        Task<FileResult> UploadFile(IFormFile file);
    }
}