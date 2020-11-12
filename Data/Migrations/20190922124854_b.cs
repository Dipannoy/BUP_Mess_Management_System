using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class b : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MealTypeId",
                table: "CustomerChoice",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerChoice_MealTypeId",
                table: "CustomerChoice",
                column: "MealTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerChoice_MealType_MealTypeId",
                table: "CustomerChoice",
                column: "MealTypeId",
                principalTable: "MealType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerChoice_MealType_MealTypeId",
                table: "CustomerChoice");

            migrationBuilder.DropIndex(
                name: "IX_CustomerChoice_MealTypeId",
                table: "CustomerChoice");

            migrationBuilder.DropColumn(
                name: "MealTypeId",
                table: "CustomerChoice");
        }
    }
}
