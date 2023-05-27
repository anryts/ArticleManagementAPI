using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserAPI.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ResolvedManyToManyIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_Channel_ChannelId",
                table: "Subscription");

            migrationBuilder.DropIndex(
                name: "IX_Subscription_ChannelId",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "ChannelId",
                table: "Subscription");

            migrationBuilder.CreateTable(
                name: "SubscriptionOnChannel",
                columns: table => new
                {
                    ChannelId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionOnChannel", x => new { x.ChannelId, x.SubscriptionId });
                    table.ForeignKey(
                        name: "FK_SubscriptionOnChannel_Channel_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriptionOnChannel_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionOnChannel_SubscriptionId",
                table: "SubscriptionOnChannel",
                column: "SubscriptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionOnChannel");

            migrationBuilder.AddColumn<Guid>(
                name: "ChannelId",
                table: "Subscription",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_ChannelId",
                table: "Subscription",
                column: "ChannelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_Channel_ChannelId",
                table: "Subscription",
                column: "ChannelId",
                principalTable: "Channel",
                principalColumn: "Id");
        }
    }
}
