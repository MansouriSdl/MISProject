using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MIS.Infrastructure.Migrations
{
    public partial class AddedEquipmentToOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EquipmentId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EquipmentId",
                table: "Orders",
                column: "EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Equipments_EquipmentId",
                table: "Orders",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Equipments_EquipmentId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_EquipmentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "Orders");
        }
    }
}
