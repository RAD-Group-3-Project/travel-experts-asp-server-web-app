using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelExpertData.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAppUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ADADFDC-411F-4AD4-BCD0-4FFA2A658206",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "00dea629-1205-416e-bb2d-5cada52310af", "95e7f4b8-5f24-4a8a-9a75-1ca211634f0c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B2FFD600-873E-4789-9A02-25EC2C37A7A1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "cf1695f7-e29d-4a06-be8a-91406c7935d7", "4c84050c-8b15-4249-ad64-2fca7daaad6a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ADADFDC-411F-4AD4-BCD0-4FFA2A658206",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2d69d1ce-d150-4864-bac8-3be9a0a33e9f", "47f63f16-3fb7-4028-b471-fdad4e1cdf2f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B2FFD600-873E-4789-9A02-25EC2C37A7A1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1a86c791-f3c2-4500-9483-8a80acd93d62", "373e847d-193a-4669-b373-3650af653333" });
        }
    }
}
