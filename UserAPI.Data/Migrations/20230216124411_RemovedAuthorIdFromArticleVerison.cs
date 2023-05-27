using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserAPI.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedAuthorIdFromArticleVerison : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleImage_Article_ArticleId",
                table: "ArticleImage");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "ArticleVersion");

            migrationBuilder.AlterColumn<Guid>(
                name: "ArticleId",
                table: "ArticleImage",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleImage_Article_ArticleId",
                table: "ArticleImage",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleImage_Article_ArticleId",
                table: "ArticleImage");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "ArticleVersion",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ArticleId",
                table: "ArticleImage",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleImage_Article_ArticleId",
                table: "ArticleImage",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id");
        }
    }
}
