using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class PreOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SetMenuId",
                table: "PreOrderSchedule",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreOrderSchedule_SetMenuId",
                table: "PreOrderSchedule",
                column: "SetMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_PreOrderSchedule_SetMenu_SetMenuId",
                table: "PreOrderSchedule",
                column: "SetMenuId",
                principalTable: "SetMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreOrderSchedule_SetMenu_SetMenuId",
                table: "PreOrderSchedule");

            migrationBuilder.DropIndex(
                name: "IX_PreOrderSchedule_SetMenuId",
                table: "PreOrderSchedule");

            migrationBuilder.DropColumn(
                name: "SetMenuId",
                table: "PreOrderSchedule");
        }
    }
}
