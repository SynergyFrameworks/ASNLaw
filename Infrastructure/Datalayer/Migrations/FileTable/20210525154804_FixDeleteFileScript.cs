using Microsoft.EntityFrameworkCore.Migrations;

namespace Datalayer.Migrations.FileTable
{
    public partial class FixDeleteFileScript : Migration
    {
        private readonly string _procedure = @"
ALTER PROCEDURE [dbo].[DeleteFile] 
	@fileDescriptionId uniqueIdentifier
AS
BEGIN
 
 Delete [ScionFileUploads] where stream_id in (Select [StreamId] from FileDescriptions where Id = @fileDescriptionId)
 Delete [FileDescriptions] where Id = @fileDescriptionId

END
";
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(_procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
