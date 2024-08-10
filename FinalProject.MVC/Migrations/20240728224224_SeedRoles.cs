using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinalProject.MVC.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "1efa27e9-a516-4f35-9dc8-a5bad1562b8a", "User" },
                    { "46193324-4fe9-45a4-b332-82ec5b53c1c5", "Guest" },
                    { "abd55edd-2c12-4dde-bb00-97ca90d765e7", "Administrator" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "1efa27e9-a516-4f35-9dc8-a5bad1562b8a");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "46193324-4fe9-45a4-b332-82ec5b53c1c5");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "abd55edd-2c12-4dde-bb00-97ca90d765e7");
        }
    }
}
