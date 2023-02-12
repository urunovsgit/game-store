using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gamestoredomain.Migrations
{
    /// <inheritdoc />
    public partial class AnotherOneMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Games",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishDate",
                table: "Games",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "PublishDate",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Games",
                newName: "Name");
        }
    }
}
