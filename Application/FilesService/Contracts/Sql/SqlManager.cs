using Scion.Datalayer.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Scion.FilesService.Contracts.Sql
{
    //TODO: Implement sql file stream...
    public class SqlManager : IFileManager
    {
        private readonly IFileRepository _fileRepository;
        public SqlManager(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public ISourceProvider Source { get; set; }

        public FileResult DeleteFile(string key)
        {
            return this.DeleteFileAsync(key).Result;
        }

        public async Task<FileResult> DeleteFileAsync(string key)
        {
            var result = await _fileRepository.DeleteFile(Guid.Parse(key));
            return new FileResult
            {
                IsSuccess = result,
                Message = result ? "Success" : "Failed to delete file"
            };
        }

        public ScionStream GetFile(string key)
        {
            return this.GetFileAsync(key).Result;
        }

        public async Task<ScionStream> GetFileAsync(string key)
        {
            var stream = new ScionStream();
            var fileDescription = _fileRepository.GetFileDescription(Guid.Parse(key));
            using (var result = new FileStream($"{fileDescription.Location}", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                await result.CopyToAsync(stream);
                stream.Position = 0;
                stream.ContentType = fileDescription.ContentType;
                stream.SourceName = fileDescription.FileName;
            }
            return stream;
        }

        public File GetFileInfo(string key)
        {
            return GetFileInfoAsync(key).Result;
        }

        public Task<File> GetFileInfoAsync(string key)
        {
            var fileDescription = _fileRepository.GetFileDescription(Guid.Parse(key));
            return Task.FromResult(
                new File
                {
                    Id = fileDescription.Id.ToString(),
                    Name = fileDescription.FileName,
                    CreatedDate = fileDescription.CreatedDate.UtcDateTime
                }
            );
        }

        public ICollection<File> GetFileInfos()
        {
            return
                _fileRepository.GetAllFileDescriptions().Select(fd => new File
                {
                    ContentType = fd.ContentType,
                    CreatedDate = fd.CreatedDate.UtcDateTime,
                    Description = fd.Description,
                    Id = fd.Id.ToString(),
                    Name = fd.FileName,
                }).ToList();
        }

        public Task<ICollection<File>> GetFileInfosAsync(int page, int itemsPerPage)
        {
            throw new NotImplementedException();
        }

        public FileResult UploadFile(Stream stream, File data)
        {
            return this.UploadFileAsync(stream, data).Result;
        }

        public FileResult UploadFile(string filePath, File data)
        {
            throw new NotImplementedException("Does not have an implementation for using a file path.");
        }

        public async Task<FileResult> UploadFileAsync(Stream stream, File data)
        {
            try
            {
                var location = $"{Source.ServerPath}\\{data.Name}";

                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);

                    await _fileRepository.AddFile(memoryStream.ToArray(), new Datalayer.Domain.FileDescription
                    {
                        ContentType = data.ContentType,
                        Location = location,
                        CreatedDate = DateTimeOffset.UtcNow,
                        Extension = System.IO.Path.GetExtension(data.Name),
                        FileName = data.Name,
                    });

                    return new FileResult()
                    {
                        IsSuccess = true,
                        Message = "Success"
                    };
                }
            }
            catch (Exception ex) when (ex is IOException || ex is DirectoryNotFoundException)
            {
                return new FileResult()
                {
                    IsSuccess = false,
                    Message = $"Failed to upload file {data.Name}."
                };
            }
        }

        public Task<FileResult> UploadFileAsync(string filePath, File data)
        {
            throw new NotImplementedException("Does not have an implementation for using a file path.");
        }
    }
}
