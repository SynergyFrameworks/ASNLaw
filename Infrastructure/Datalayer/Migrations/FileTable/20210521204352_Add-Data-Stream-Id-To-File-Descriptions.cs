using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Datalayer.Migrations.FileTable
{
    public partial class AddDataStreamIdToFileDescriptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StreamId",
                table: "FileDescriptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StreamId",
                table: "FileDescriptions");
        }
    }
}
