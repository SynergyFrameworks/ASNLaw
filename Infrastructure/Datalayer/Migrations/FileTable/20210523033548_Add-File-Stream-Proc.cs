using Microsoft.EntityFrameworkCore.Migrations;

namespace Datalayer.Migrations.FileTable
{
    public partial class AddFileStreamProc : Migration
    {
        private readonly string _procedure =
  @"CREATE PROCEDURE [dbo].[UploadFile] 
	@fileName nvarchar(255),
	@fileContent varbinary(max),
	@description nvarchar(255),
	@contentType nvarchar(max),
	@extension nvarchar(4),
	@location nvarchar(100),
	@createdBy nvarchar(50),
	@streamId uniqueIdentifier output
AS
BEGIN

DECLARE @command nvarchar(1000)
DECLARE @newId uniqueidentifier

set @command = N'
Declare @streamId Table ([id] uniqueidentifier)

INSERT INTO [dbo].[ScionFileUploads] ([name],[file_stream])
OUTPUT Inserted.stream_id INTO @streamId
SELECT @fileName, @fileContent as FileData
	 
SELECT @newId = [id] from @streamId
'

EXEC sp_executesql @command, N'@fileName nvarchar(255), @fileContent varbinary(max), @newId uniqueidentifier OUTPUT', @fileName = @fileName, @fileContent = @fileContent, @newId = @newId OUTPUT

Select @streamId = @newId

INSERT INTO [dbo].[FileDescriptions]
           ([Id]
           ,[FileName]
           ,[Description]
           ,[Location]
           ,[ContentType]
           ,[Extension]
           ,[CreatedDate]
           ,[CreatedBy]
           ,[StreamId])
Select NEWID(), @fileName, @description
	  , @location, @contentType, @extension
      , GETUTCDATE(), '', @streamId

END";
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(_procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[UploadFile]");
        }
    }
}
