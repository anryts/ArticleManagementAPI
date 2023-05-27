using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace ArticleAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedAnotherVector : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVector",
                table: "ArticleTag",
                type: "tsvector",
                nullable: false)
                .Annotation("Npgsql:TsVectorConfig", "english")
                .Annotation("Npgsql:TsVectorProperties", new[] { "TagName" });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTag_SearchVector",
                table: "ArticleTag",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ArticleTag_SearchVector",
                table: "ArticleTag");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                table: "ArticleTag");
        }
    }
}
