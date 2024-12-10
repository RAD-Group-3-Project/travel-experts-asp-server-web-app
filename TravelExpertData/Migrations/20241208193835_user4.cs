using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelExpertData.Migrations
{
    /// <inheritdoc />
    public partial class user4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ADADFDC-411F-4AD4-BCD0-4FFA2A658206",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "93539569-63f1-4ac0-b0fd-0de0a81d7c2e", "0fe384b9-0e32-44f5-a7f4-0d4d9610898e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B2FFD600-873E-4789-9A02-25EC2C37A7A1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2b2823af-20b4-4277-8cb6-64879a462f50", "35185425-d484-4105-9b43-464487e4f4ff" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
