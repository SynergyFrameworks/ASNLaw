using Microsoft.EntityFrameworkCore.Migrations;

namespace Datalayer.Migrations.FileTable
{
    public partial class AddFileTable : Migration
    {
        private string _configuration;

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _configuration =
            $@" 
                CREATE TABLE ScionFileUploads AS FileTable
                WITH
                (FileTable_Directory = 'ScionFileUploads');
                GO
            ";
            migrationBuilder.Sql(_configuration, true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _configuration =
$@" 
                CREATE TABLE ScionFileUploads
                GO
            ";
            migrationBuilder.Sql(_configuration, true);
        }
    }
}
