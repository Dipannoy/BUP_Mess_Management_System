using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class Testing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ExtraItemId",
                table: "SetMenuDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SetMenuDetails_ExtraItemId",
                table: "SetMenuDetails",
                column: "ExtraItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_SetMenuDetails_ExtraItem_ExtraItemId",
                table: "SetMenuDetails",
                column: "ExtraItemId",
                principalTable: "ExtraItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetMenuDetails_ExtraItem_ExtraItemId",
                table: "SetMenuDetails");

            migrationBuilder.DropIndex(
                name: "IX_SetMenuDetails_ExtraItemId",
                table: "SetMenuDetails");

            migrationBuilder.DropColumn(
                name: "ExtraItemId",
                table: "SetMenuDetails");
        }
    }
}
