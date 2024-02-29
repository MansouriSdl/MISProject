using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MIS.Infrastructure.Migrations
{
    public partial class MovedConsumptionTypeToEquipmentsTableInsteadOfEquipmentTypesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTypes_ConsumptionTypes_ConsumptionTypeId",
                table: "EquipmentTypes");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentTypes_ConsumptionTypeId",
                table: "EquipmentTypes");

            migrationBuilder.DropColumn(
                name: "ConsumptionTypeId",
                table: "EquipmentTypes");

            migrationBuilder.AddColumn<Guid>(
                name: "ConsumptionTypeId",
                table: "Equipments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_ConsumptionTypeId",
                table: "Equipments",
                column: "ConsumptionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_ConsumptionTypes_ConsumptionTypeId",
                table: "Equipments",
                column: "ConsumptionTypeId",
                principalTable: "ConsumptionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_ConsumptionTypes_ConsumptionTypeId",
                table: "Equipments");

            migrationBuilder.DropIndex(
                name: "IX_Equipments_ConsumptionTypeId",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "ConsumptionTypeId",
                table: "Equipments");

            migrationBuilder.AddColumn<Guid>(
                name: "ConsumptionTypeId",
                table: "EquipmentTypes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentTypes_ConsumptionTypeId",
                table: "EquipmentTypes",
                column: "ConsumptionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTypes_ConsumptionTypes_ConsumptionTypeId",
                table: "EquipmentTypes",
                column: "ConsumptionTypeId",
                principalTable: "ConsumptionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
