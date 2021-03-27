using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class MenuAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MenuItemId",
                table: "CustomerChoiceV2",
                nullable: true,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerChoiceV2_MenuItemId",
                table: "CustomerChoiceV2",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerChoiceV2_MenuItem_MenuItemId",
                table: "CustomerChoiceV2",
                column: "MenuItemId",
                principalTable: "MenuItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerChoiceV2_MenuItem_MenuItemId",
                table: "CustomerChoiceV2");

            migrationBuilder.DropIndex(
                name: "IX_CustomerChoiceV2_MenuItemId",
                table: "CustomerChoiceV2");

            migrationBuilder.DropColumn(
                name: "MenuItemId",
                table: "CustomerChoiceV2");
        }
    }
}
