using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinalProject.MVC.Migrations
{
    /// <inheritdoc />
    public partial class SeedDepartments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "097da06f-17d5-49a2-8978-c906f15d9d2c", "Information Technology" },
                    { "141ca7d3-eb8c-42be-88e1-26736c4d9a5d", "Customer Service" },
                    { "8c1da1e6-69a5-4fa1-b3f4-2ac0d3267eca", "Marketing" },
                    { "cb1f5fb4-d88f-4c0a-989f-de464d080221", "Finance" },
                    { "f3f07fd9-7e29-4ea7-9e2c-b16928da5fb3", "Human Resources" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: "097da06f-17d5-49a2-8978-c906f15d9d2c");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: "141ca7d3-eb8c-42be-88e1-26736c4d9a5d");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: "8c1da1e6-69a5-4fa1-b3f4-2ac0d3267eca");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: "cb1f5fb4-d88f-4c0a-989f-de464d080221");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: "f3f07fd9-7e29-4ea7-9e2c-b16928da5fb3");
        }
    }
}
