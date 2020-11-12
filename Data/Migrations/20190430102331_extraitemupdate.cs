using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class extraitemupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StoreOutItemId",
                table: "ExtraItem",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ExtraItem_StoreOutItemId",
                table: "ExtraItem",
                column: "StoreOutItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraItem_StoreOutItem_StoreOutItemId",
                table: "ExtraItem",
                column: "StoreOutItemId",
                principalTable: "StoreOutItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraItem_StoreOutItem_StoreOutItemId",
                table: "ExtraItem");

            migrationBuilder.DropIndex(
                name: "IX_ExtraItem_StoreOutItemId",
                table: "ExtraItem");

            migrationBuilder.DropColumn(
                name: "StoreOutItemId",
                table: "ExtraItem");
        }
    }
}
