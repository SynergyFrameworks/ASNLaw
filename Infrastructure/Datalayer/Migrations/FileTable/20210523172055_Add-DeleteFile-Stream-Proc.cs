using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Datalayer.Migrations.FileTable
{
    public partial class AddDeleteFileStreamProc : Migration
    {
        private readonly string _procedure = @"
CREATE PROCEDURE [dbo].[DeleteFile] 
	@fileDescriptionId uniqueIdentifier
AS
BEGIN
 
 Delete [ScionFileUploads] where stream_id = (Select [stream_id] from FileDescriptions where Id = @fileDescriptionId)
 Delete [FileDescriptions] where Id = @fileDescriptionId

END
";
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(_procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[DeleteFile]");
        }
    }
}
