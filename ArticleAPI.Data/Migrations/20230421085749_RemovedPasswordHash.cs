﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArticleAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedPasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "User");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
