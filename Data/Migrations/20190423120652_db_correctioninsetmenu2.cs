using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class db_correctioninsetmenu2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MealTypeId",
                table: "SetMenu",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SetMenu_MealTypeId",
                table: "SetMenu",
                column: "MealTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SetMenu_MealType_MealTypeId",
                table: "SetMenu",
                column: "MealTypeId",
                principalTable: "MealType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetMenu_MealType_MealTypeId",
                table: "SetMenu");

            migrationBuilder.DropIndex(
                name: "IX_SetMenu_MealTypeId",
                table: "SetMenu");

            migrationBuilder.DropColumn(
                name: "MealTypeId",
                table: "SetMenu");
        }
    }
}
