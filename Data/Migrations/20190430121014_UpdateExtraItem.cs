using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class UpdateExtraItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraItem_StoreOutItem_StoreOutItemId",
                table: "ExtraItem");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraItem_StoreOutItem_StoreOutItemId",
                table: "ExtraItem",
                column: "StoreOutItemId",
                principalTable: "StoreOutItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraItem_StoreOutItem_StoreOutItemId",
                table: "ExtraItem");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraItem_StoreOutItem_StoreOutItemId",
                table: "ExtraItem",
                column: "StoreOutItemId",
                principalTable: "StoreOutItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
