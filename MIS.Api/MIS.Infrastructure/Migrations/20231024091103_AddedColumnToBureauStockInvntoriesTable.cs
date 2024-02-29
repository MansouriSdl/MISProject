using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MIS.Infrastructure.Migrations
{
    public partial class AddedColumnToBureauStockInvntoriesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "BureauStockInventories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarketObject",
                table: "BureauStockInventories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarketReference",
                table: "BureauStockInventories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierId",
                table: "BureauStockInventories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BureauStockInventories_SupplierId",
                table: "BureauStockInventories",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_BureauStockInventories_Suppliers_SupplierId",
                table: "BureauStockInventories",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BureauStockInventories_Suppliers_SupplierId",
                table: "BureauStockInventories");

            migrationBuilder.DropIndex(
                name: "IX_BureauStockInventories_SupplierId",
                table: "BureauStockInventories");

            migrationBuilder.DropColumn(
                name: "Designation",
                table: "BureauStockInventories");

            migrationBuilder.DropColumn(
                name: "MarketObject",
                table: "BureauStockInventories");

            migrationBuilder.DropColumn(
                name: "MarketReference",
                table: "BureauStockInventories");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "BureauStockInventories");
        }
    }
}
