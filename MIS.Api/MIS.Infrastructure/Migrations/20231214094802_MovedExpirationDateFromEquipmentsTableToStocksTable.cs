using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MIS.Infrastructure.Migrations
{
    public partial class MovedExpirationDateFromEquipmentsTableToStocksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Equipments");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Stocks",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Stocks");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Equipments",
                type: "datetime2",
                nullable: true);
        }
    }
}
