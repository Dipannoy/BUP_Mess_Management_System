using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class AddSTOWarehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StoreOutItemId",
                table: "WarehouseStorage",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseStorage_StoreOutItemId",
                table: "WarehouseStorage",
                column: "StoreOutItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseStorage_StoreOutItem_StoreOutItemId",
                table: "WarehouseStorage",
                column: "StoreOutItemId",
                principalTable: "StoreOutItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseStorage_StoreOutItem_StoreOutItemId",
                table: "WarehouseStorage");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseStorage_StoreOutItemId",
                table: "WarehouseStorage");

            migrationBuilder.DropColumn(
                name: "StoreOutItemId",
                table: "WarehouseStorage");
        }
    }
}
