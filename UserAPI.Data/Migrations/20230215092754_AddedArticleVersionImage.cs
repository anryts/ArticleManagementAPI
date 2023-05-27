using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserAPI.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedArticleVersionImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleImage_ArticleVersion_ArticleVersionId",
                table: "ArticleImage");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleImage_Article_ArticleId",
                table: "ArticleImage");

            migrationBuilder.DropIndex(
                name: "IX_ArticleImage_ArticleVersionId",
                table: "ArticleImage");

            migrationBuilder.DropColumn(
                name: "ArticleVersionId",
                table: "ArticleImage");

            migrationBuilder.AlterColumn<Guid>(
                name: "ArticleId",
                table: "ArticleImage",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateTable(
                name: "ArticleVersionImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ArticleVersionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImagePath = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleVersionImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleVersionImage_ArticleVersion_ArticleVersionId",
                        column: x => x.ArticleVersionId,
                        principalTable: "ArticleVersion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleVersionImage_ArticleVersionId",
                table: "ArticleVersionImage",
                column: "ArticleVersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleImage_Article_ArticleId",
                table: "ArticleImage",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleImage_Article_ArticleId",
                table: "ArticleImage");

            migrationBuilder.DropTable(
                name: "ArticleVersionImage");

            migrationBuilder.AlterColumn<Guid>(
                name: "ArticleId",
                table: "ArticleImage",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ArticleVersionId",
                table: "ArticleImage",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleImage_ArticleVersionId",
                table: "ArticleImage",
                column: "ArticleVersionId");

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
    }
}
