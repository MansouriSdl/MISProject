using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MIS.Infrastructure.Migrations
{
    public partial class AddedColumnsToEquipmentsAndBureauStockInventoriesTablesAndRemovedDescriptionColumnFromStocksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Stocks");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Equipments",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Equipments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "Equipments",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentState",
                table: "BureauStockInventories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "EquipmentState",
                table: "BureauStockInventories");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Stocks",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
