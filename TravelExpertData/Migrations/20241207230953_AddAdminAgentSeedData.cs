using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelExpertData.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminAgentSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CustomerId", "Email", "EmailConfirmed", "IsAdmin", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "9ADADFDC-411F-4AD4-BCD0-4FFA2A658206", 0, "2d69d1ce-d150-4864-bac8-3be9a0a33e9f", null, null, false, false, false, null, null, null, "agent", null, false, "47f63f16-3fb7-4028-b471-fdad4e1cdf2f", false, "agent" },
                    { "B2FFD600-873E-4789-9A02-25EC2C37A7A1", 0, "1a86c791-f3c2-4500-9483-8a80acd93d62", null, null, false, true, false, null, null, null, "admin", null, false, "373e847d-193a-4669-b373-3650af653333", false, "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ADADFDC-411F-4AD4-BCD0-4FFA2A658206");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B2FFD600-873E-4789-9A02-25EC2C37A7A1");
        }
    }
}
