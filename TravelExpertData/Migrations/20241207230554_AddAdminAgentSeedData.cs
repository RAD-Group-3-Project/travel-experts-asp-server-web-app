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
                    { "9ADADFDC-411F-4AD4-BCD0-4FFA2A658206", 0, "063cce3e-00af-4c07-b8fb-4ad82f74c166", null, null, false, false, false, null, null, null, "agent", null, false, "fe2c0667-c2cd-4691-b9e1-a15aa00f798c", false, "agent" },
                    { "B2FFD600-873E-4789-9A02-25EC2C37A7A1", 0, "c46403a6-318a-4934-aebb-5a813b8c7509", null, null, false, false, false, null, null, null, "admin", null, false, "46ac0fe0-4f2a-4b3a-bca0-cd4db7e8dcb7", false, "admin" }
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
