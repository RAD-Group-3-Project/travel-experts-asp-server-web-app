using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelExpertData.Migrations
{
    /// <inheritdoc />
    public partial class AddPkgImg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PkgImage",
                table: "Packages",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ADADFDC-411F-4AD4-BCD0-4FFA2A658206",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f53af150-036b-4165-b928-223028bc32d8", "ddf97509-8d8a-400f-be82-3f9228ad7cf4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B2FFD600-873E-4789-9A02-25EC2C37A7A1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b5e8d5b9-acf0-43cc-866a-883578ecd872", "b36706a2-9573-4a62-830d-d433f58028b7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PkgImage",
                table: "Packages");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ADADFDC-411F-4AD4-BCD0-4FFA2A658206",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "859202c3-4152-490e-add5-80f3cafb4d90", "ba256e4a-b094-4631-b322-c4793e912b87" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B2FFD600-873E-4789-9A02-25EC2C37A7A1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "14746f66-3d07-4489-9c87-2b412fdd304f", "9b7da68d-50dd-4785-ab5c-5208f30abdd7" });
        }
    }
}
