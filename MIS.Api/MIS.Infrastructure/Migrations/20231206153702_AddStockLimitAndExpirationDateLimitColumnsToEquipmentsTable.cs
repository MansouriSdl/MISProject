using Microsoft.EntityFrameworkCore.Migrations;

namespace MIS.Infrastructure.Migrations
{
    public partial class AddStockLimitAndExpirationDateLimitColumnsToEquipmentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpirationDateLimit",
                table: "Equipments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StockLimit",
                table: "Equipments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationDateLimit",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "StockLimit",
                table: "Equipments");
        }
    }
}
