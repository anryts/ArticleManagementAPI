using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedPastDue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPastDue",
                table: "Subscription",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPastDue",
                table: "Subscription");
        }
    }
}
