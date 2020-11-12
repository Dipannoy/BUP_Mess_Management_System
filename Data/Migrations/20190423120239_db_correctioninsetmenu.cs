using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class db_correctioninsetmenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetMenuDetails_MealType_MealTypeId",
                table: "SetMenuDetails");

            migrationBuilder.DropIndex(
                name: "IX_SetMenuDetails_MealTypeId",
                table: "SetMenuDetails");

            migrationBuilder.DropColumn(
                name: "MealTypeId",
                table: "SetMenuDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MealTypeId",
                table: "SetMenuDetails",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SetMenuDetails_MealTypeId",
                table: "SetMenuDetails",
                column: "MealTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SetMenuDetails_MealType_MealTypeId",
                table: "SetMenuDetails",
                column: "MealTypeId",
                principalTable: "MealType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
