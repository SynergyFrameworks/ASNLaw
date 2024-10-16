using System.Threading.Tasks;

namespace Scion.FilesService.Contracts
{
    /// <summary>
    /// Definition of a file manager.
    /// </summary>
    /// <typeparam name="object"></typeparam>
    public interface IFileManager
    {
        ISourceProvider Source { get; set; }

        ScionStream GetFile(string key);
        Task<ScionStream> GetFileAsync(string key);

        System.Collections.Generic.ICollection<File> GetFileInfos();
        Task<System.Collections.Generic.ICollection<File>> GetFileInfosAsync(int page, int itemsPerPage);

        File GetFileInfo(string key);
        Task<File> GetFileInfoAsync(string key);

        FileResult DeleteFile(string key);
        Task<FileResult> DeleteFileAsync(string key);

        FileResult UploadFile(System.IO.Stream stream, File data);
        FileResult UploadFile(string filePath, File data);

        Task<FileResult> UploadFileAsync(System.IO.Stream stream, File data);
        Task<FileResult> UploadFileAsync(string filePath, File data);
    }
}
