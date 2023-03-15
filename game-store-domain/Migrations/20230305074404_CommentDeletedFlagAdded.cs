﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gamestoredomain.Migrations
{
    /// <inheritdoc />
    public partial class CommentDeletedFlagAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Comment",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Comment");
        }
    }
}