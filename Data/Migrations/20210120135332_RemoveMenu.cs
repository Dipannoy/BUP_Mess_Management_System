using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class RemoveMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerChoiceV2_MenuItem_MenuItemId",
                table: "CustomerChoiceV2");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerDailyMenuChoice_MenuItem_MenuItemId",
                table: "CustomerDailyMenuChoice");

            migrationBuilder.DropIndex(
                name: "IX_CustomerDailyMenuChoice_MenuItemId",
                table: "CustomerDailyMenuChoice");

            migrationBuilder.DropIndex(
                name: "IX_CustomerChoiceV2_MenuItemId",
                table: "CustomerChoiceV2");

            migrationBuilder.DropColumn(
                name: "MenuItemId",
                table: "CustomerDailyMenuChoice");

            migrationBuilder.DropColumn(
                name: "MenuItemId",
                table: "CustomerChoiceV2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MenuItemId",
                table: "CustomerDailyMenuChoice",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MenuItemId",
                table: "CustomerChoiceV2",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDailyMenuChoice_MenuItemId",
                table: "CustomerDailyMenuChoice",
                column: "MenuItemId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerDailyMenuChoice_MenuItem_MenuItemId",
                table: "CustomerDailyMenuChoice",
                column: "MenuItemId",
                principalTable: "MenuItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
