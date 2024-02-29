using Microsoft.EntityFrameworkCore.Migrations;

namespace MIS.Infrastructure.Migrations
{
    public partial class ChangedOrdersAndRequestsRelationshipToOneToOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DivisionEquipmentRequests_OrderId",
                table: "DivisionEquipmentRequests");

            migrationBuilder.CreateIndex(
                name: "IX_DivisionEquipmentRequests_OrderId",
                table: "DivisionEquipmentRequests",
                column: "OrderId",
                unique: true,
                filter: "[OrderId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DivisionEquipmentRequests_OrderId",
                table: "DivisionEquipmentRequests");

            migrationBuilder.CreateIndex(
                name: "IX_DivisionEquipmentRequests_OrderId",
                table: "DivisionEquipmentRequests",
                column: "OrderId");
        }
    }
}
