using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserAPI.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserJob",
                columns: table => new
                {
                    JobId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserJob", x => new { x.UserId, x.JobId });
                    table.ForeignKey(
                        name: "FK_UserJob_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserJob_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Gender", "Name" },
                values: new object[,]
                {
                    { new Guid("091f4f08-9e8f-434d-ad24-c841860a0e14"), "test15@mail.com", 2, "Hermione" },
                    { new Guid("1020d434-9734-4081-9532-95667395b71e"), "test5@mail.com", 1, "Craig" },
                    { new Guid("2e2aa0b3-96c2-4124-b41a-7f668577dde1"), "test7@mail.com", 1, "Junaid" },
                    { new Guid("3c62bf56-9005-41ec-af94-34f4e60a6913"), "test14@mail.com", 2, "Kiera" },
                    { new Guid("3e2e62fc-aa31-48b0-9c4a-e429f81efba6"), "test12@mail.com", 2, "Edie" },
                    { new Guid("403d7dc1-7b2e-4fb8-b692-855100f27e92"), "test4@mail.com", 1, "Abu" },
                    { new Guid("5839bbe9-130c-452e-a3f5-48a06e19b98b"), "test17@mail.com", 2, "Cecil" },
                    { new Guid("694d119e-8951-476b-8675-e50b6f0e4595"), "test3@mail.com", 1, "Roy " },
                    { new Guid("6b5763f9-0e31-41d3-8d07-7006cd8cc060"), "test11@mail.com", 2, "Emily" },
                    { new Guid("7438e5e7-6d25-4f6a-9bb7-2741c61470e7"), "test8@mail.com", 1, "Ieuan" },
                    { new Guid("8fd66ae6-d36d-4a0a-9f7d-5d7797d6fc58"), "test1@mail.com", 1, "Anton" },
                    { new Guid("9a191c8d-086c-460d-a7c1-b27dfe149890"), "test9@mail.com", 1, "Anika" },
                    { new Guid("afa67de6-3647-4a90-b859-502397dd6e8e"), "test10@mail.com", 1, "Jago" },
                    { new Guid("bf43b705-44f3-4dc2-9345-56c3bdc58788"), "test20@mail.com", 2, "Leyla" },
                    { new Guid("bf7a0302-0b03-4e65-a91e-edddf80752f1"), "test19@mail.com", 2, "Mathilda" },
                    { new Guid("c442cf8e-1d97-487e-a0b0-5f42a205b3b5"), "test6@mail.com", 1, "Francesco" },
                    { new Guid("eedbe32f-05a5-4355-9707-65933032dd7b"), "test2@mail.com", 1, "Oliwier" },
                    { new Guid("fbf1f613-51ad-48fa-841b-6b13dd321758"), "test13@mail.com", 2, "Anaya" },
                    { new Guid("fc2c999e-720d-47b3-aeb6-b6e214486406"), "test16@mail.com", 2, "Maariyah" },
                    { new Guid("fe96fe91-c809-47da-92b6-9a6ca1cd29f4"), "test18@mail.com", 2, "Aleesha" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserJob_JobId",
                table: "UserJob",
                column: "JobId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserJob");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
