using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class db_correctioninsetmenu3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetMenu_MealType_MealTypeId",
                table: "SetMenu");

            migrationBuilder.AddForeignKey(
                name: "FK_SetMenu_MealType_MealTypeId",
                table: "SetMenu",
                column: "MealTypeId",
                principalTable: "MealType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetMenu_MealType_MealTypeId",
                table: "SetMenu");

            migrationBuilder.AddForeignKey(
                name: "FK_SetMenu_MealType_MealTypeId",
                table: "SetMenu",
                column: "MealTypeId",
                principalTable: "MealType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
