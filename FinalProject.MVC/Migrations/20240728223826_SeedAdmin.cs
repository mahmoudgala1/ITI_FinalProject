using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinalProject.MVC.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DepartmentId", "Email", "FName", "Image", "LName", "Password", "Phone" },
                values: new object[] { "be0cd402-3c1f-434b-b83d-e2d5038f31b9", "097da06f-17d5-49a2-8978-c906f15d9d2c", "admin@test.com", "Admin", null, "Admin", "admin", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: "be0cd402-3c1f-434b-b83d-e2d5038f31b9");
        }
    }
}
