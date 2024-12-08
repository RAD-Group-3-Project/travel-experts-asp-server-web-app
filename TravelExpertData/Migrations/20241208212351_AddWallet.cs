using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelExpertData.Migrations
{
    /// <inheritdoc />
    public partial class AddWallet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ADADFDC-411F-4AD4-BCD0-4FFA2A658206",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "72c6e5ac-cf74-46e9-b7de-87f372744bae", "0ac98df9-612d-4597-8a59-27ab926f8b14" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "B2FFD600-873E-4789-9A02-25EC2C37A7A1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a5f52499-b2b6-4d01-b7e4-3ab393696663", "7326c651-481d-4931-8b39-7538f496046f" });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_CustomerId",
                table: "Wallets",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Wallets");

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
