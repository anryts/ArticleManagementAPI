using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserAPI.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedArticleVersionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleImages_Articles_ArticleId",
                table: "ArticleImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Articles",
                table: "Articles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleImages",
                table: "ArticleImages");

            migrationBuilder.RenameTable(
                name: "Articles",
                newName: "Article");

            migrationBuilder.RenameTable(
                name: "ArticleImages",
                newName: "ArticleImage");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleImages_ArticleId",
                table: "ArticleImage",
                newName: "IX_ArticleImage_ArticleId");

            migrationBuilder.AddColumn<Guid>(
                name: "ArticleVersionId",
                table: "ArticleImage",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Article",
                table: "Article",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleImage",
                table: "ArticleImage",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ArticleVersion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ArticleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    ContentPath = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleVersion_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleImage_ArticleVersionId",
                table: "ArticleImage",
                column: "ArticleVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleVersion_ArticleId",
                table: "ArticleVersion",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleImage_ArticleVersion_ArticleVersionId",
                table: "ArticleImage",
                column: "ArticleVersionId",
                principalTable: "ArticleVersion",
                principalColumn: "Id");

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
                name: "FK_ArticleImage_ArticleVersion_ArticleVersionId",
                table: "ArticleImage");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleImage_Article_ArticleId",
                table: "ArticleImage");

            migrationBuilder.DropTable(
                name: "ArticleVersion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleImage",
                table: "ArticleImage");

            migrationBuilder.DropIndex(
                name: "IX_ArticleImage_ArticleVersionId",
                table: "ArticleImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Article",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "ArticleVersionId",
                table: "ArticleImage");

            migrationBuilder.RenameTable(
                name: "ArticleImage",
                newName: "ArticleImages");

            migrationBuilder.RenameTable(
                name: "Article",
                newName: "Articles");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleImage_ArticleId",
                table: "ArticleImages",
                newName: "IX_ArticleImages_ArticleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleImages",
                table: "ArticleImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Articles",
                table: "Articles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleImages_Articles_ArticleId",
                table: "ArticleImages",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
