using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserAPI.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("07de848c-62fb-4183-92b8-cf0df0f6ee1c"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("1009ffce-e508-47bd-8879-13c70fcd0fc4"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("139554e3-7ba6-4b36-bcb6-1b7fd05bbeb7"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("1bf2ba60-d801-4a9a-8fe8-2a5d6f5d569b"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("1e099f22-d894-430a-9f48-85e8edd9aa8e"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("2109a1ef-183d-4b8f-b454-b6a33c5a3d03"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("355fef8d-eeaf-46eb-8b82-07ec5f4598c2"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("47a1109b-548e-4d7e-855a-8c50807286d8"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("4fb797ea-99d5-4849-b73d-dbf7749dfd71"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("4fd1549e-6bca-46f2-b1ff-970c515fcd0d"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("7f5aee4b-1397-42fd-af48-dca936f87b54"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("82533341-26b0-4159-b9cd-468baa8c8a91"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("a600e36e-1ef9-49c5-a033-7e9b206b9f2a"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("a811529b-20e0-4479-96b3-0582fa9a7e9c"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("a95b99a7-df74-4c5c-8842-2127983adafb"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("d6e8c8f5-4206-4ba8-b2e1-f1ae94645747"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("dfc30086-de73-4bf7-9638-696f956afce4"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("f5a041f7-5f76-4471-bfd3-5c2d307f8ebc"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("f6d2bd1d-cc80-4083-900d-e939477f672e"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("fdfdc8f8-6eba-4372-8acc-e0685714f0f7"));

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "User");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Gender", "Name" },
                values: new object[,]
                {
                    { new Guid("07de848c-62fb-4183-92b8-cf0df0f6ee1c"), "test10@mail.com", 1, "Jago" },
                    { new Guid("1009ffce-e508-47bd-8879-13c70fcd0fc4"), "test18@mail.com", 2, "Aleesha" },
                    { new Guid("139554e3-7ba6-4b36-bcb6-1b7fd05bbeb7"), "test3@mail.com", 1, "Roy " },
                    { new Guid("1bf2ba60-d801-4a9a-8fe8-2a5d6f5d569b"), "test16@mail.com", 2, "Maariyah" },
                    { new Guid("1e099f22-d894-430a-9f48-85e8edd9aa8e"), "test9@mail.com", 1, "Anika" },
                    { new Guid("2109a1ef-183d-4b8f-b454-b6a33c5a3d03"), "test15@mail.com", 2, "Hermione" },
                    { new Guid("355fef8d-eeaf-46eb-8b82-07ec5f4598c2"), "test1@mail.com", 1, "Anton" },
                    { new Guid("47a1109b-548e-4d7e-855a-8c50807286d8"), "test8@mail.com", 1, "Ieuan" },
                    { new Guid("4fb797ea-99d5-4849-b73d-dbf7749dfd71"), "test7@mail.com", 1, "Junaid" },
                    { new Guid("4fd1549e-6bca-46f2-b1ff-970c515fcd0d"), "test13@mail.com", 2, "Anaya" },
                    { new Guid("7f5aee4b-1397-42fd-af48-dca936f87b54"), "test20@mail.com", 2, "Leyla" },
                    { new Guid("82533341-26b0-4159-b9cd-468baa8c8a91"), "test11@mail.com", 2, "Emily" },
                    { new Guid("a600e36e-1ef9-49c5-a033-7e9b206b9f2a"), "test17@mail.com", 2, "Cecil" },
                    { new Guid("a811529b-20e0-4479-96b3-0582fa9a7e9c"), "test4@mail.com", 1, "Abu" },
                    { new Guid("a95b99a7-df74-4c5c-8842-2127983adafb"), "test14@mail.com", 2, "Kiera" },
                    { new Guid("d6e8c8f5-4206-4ba8-b2e1-f1ae94645747"), "test12@mail.com", 2, "Edie" },
                    { new Guid("dfc30086-de73-4bf7-9638-696f956afce4"), "test19@mail.com", 2, "Mathilda" },
                    { new Guid("f5a041f7-5f76-4471-bfd3-5c2d307f8ebc"), "test2@mail.com", 1, "Oliwier" },
                    { new Guid("f6d2bd1d-cc80-4083-900d-e939477f672e"), "test6@mail.com", 1, "Francesco" },
                    { new Guid("fdfdc8f8-6eba-4372-8acc-e0685714f0f7"), "test5@mail.com", 1, "Craig" }
                });
        }
    }
}
