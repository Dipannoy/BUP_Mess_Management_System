using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class OrderHistoryModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreOutItem_UnitType_UnitTypeId",
                table: "StoreOutItem");

            migrationBuilder.DropForeignKey(
                name: "FK_SubMenu_Menu_MenuId",
                table: "SubMenu");

            migrationBuilder.AlterColumn<long>(
                name: "StoreOutItemId",
                table: "OrderHistory",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_StoreOutItem_UnitType_UnitTypeId",
                table: "StoreOutItem",
                column: "UnitTypeId",
                principalTable: "UnitType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubMenu_Menu_MenuId",
                table: "SubMenu",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreOutItem_UnitType_UnitTypeId",
                table: "StoreOutItem");

            migrationBuilder.DropForeignKey(
                name: "FK_SubMenu_Menu_MenuId",
                table: "SubMenu");

            migrationBuilder.AlterColumn<long>(
                name: "StoreOutItemId",
                table: "OrderHistory",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreOutItem_UnitType_UnitTypeId",
                table: "StoreOutItem",
                column: "UnitTypeId",
                principalTable: "UnitType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubMenu_Menu_MenuId",
                table: "SubMenu",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
