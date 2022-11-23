using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GameID", "Description", "Developer", "Genre", "ImageURL", "Name", "Platform", "Price" },
                values: new object[,]
                {
                    { 1, "1 player is the imposter, the others do tasks.", "InnerSloth", 2, "test", "Among us", 0, 19.989999999999998 },
                    { 2, "Battle royal shooter thing", "Respawn Entertainment", 0, "test", "Apex Legends", 0, 0.0 },
                    { 3, "Online shooter", "Dice", 0, "test", "Battlefield 2042", 0, 59.990000000000002 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "GameID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "GameID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "GameID",
                keyValue: 3);
        }
    }
}
