using Microsoft.EntityFrameworkCore;
using Datalayer.Context;
using Datalayer.Contracts;
using Datalayer.Domain;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Datalayer.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly FileTableDbContext _context;
        public FileRepository(FileTableDbContext context)
        {
            _context = context;
        }


        public async Task<Guid> AddFile(byte[] fileContent, FileDescription description)
        {
            string query =
                @"EXEC [dbo].[UploadFile]
		@fileName = {0},
		@fileContent = {1},
		@location = {2},
		@description = {3},
		@contentType = {4},
		@extension = {5},
		@createdBy = {6},
		@streamId = {7} OUTPUT";

            var schemaParameter = new SqlParameter("@streamId", System.Data.SqlDbType.UniqueIdentifier);
            schemaParameter.Direction = System.Data.ParameterDirection.Output;

            await _context.Database.ExecuteSqlRawAsync(query
                  , description.FileName
                  , fileContent
                  , description.Location
                  , description.Description
                  , description.ContentType
                  , description.Extension
                  , description.CreatedBy
                  , schemaParameter
                  );

            return (Guid)schemaParameter.Value;
        }

        public IEnumerable<FileDescription> GetAllFileDescriptions()
        {
            return _context.FileDescriptions;
        }

        //TODO: Refactor to return pagination information...
        public async Task<IEnumerable<FileDescription>> GetAllFileDescriptions(int pageNumber, int itemsPerPage)
        {
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            var totalCount = await _context.FileDescriptions.CountAsync();
            var skipPageCount = itemsPerPage * pageNumber - itemsPerPage;
            return _context.FileDescriptions
                .Skip(skipPageCount > totalCount ? totalCount : skipPageCount)
                .Take(itemsPerPage);
        }

        public FileDescription GetFileDescription(Guid descriptionId)
        {
            return _context.FileDescriptions.Where(fd => fd.Id == descriptionId).First();
        }

        public IEnumerable<FileDescription> GetFileDescriptions(ICollection<Guid> guids)
        {
            return _context.FileDescriptions.Where(fd => guids.Contains(fd.Id));
        }

        public async Task<bool> DeleteFile(Guid descriptionId)
        {
            string procedure = "EXEC [dbo].[DeleteFile]	@fileDescriptionId = {0}";
            var affectedRows = await _context.Database.ExecuteSqlRawAsync(procedure, descriptionId);

            return affectedRows > 0;
        }

    }
}
