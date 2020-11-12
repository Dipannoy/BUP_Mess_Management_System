using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class ExtraItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "SetMenuId",
                table: "ExtraItem",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "MealTypeId",
                table: "ExtraItem",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MenuDate",
                table: "ExtraItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExtraItem_MealTypeId",
                table: "ExtraItem",
                column: "MealTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraItem_MealType_MealTypeId",
                table: "ExtraItem",
                column: "MealTypeId",
                principalTable: "MealType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraItem_MealType_MealTypeId",
                table: "ExtraItem");

            migrationBuilder.DropIndex(
                name: "IX_ExtraItem_MealTypeId",
                table: "ExtraItem");

            migrationBuilder.DropColumn(
                name: "MealTypeId",
                table: "ExtraItem");

            migrationBuilder.DropColumn(
                name: "MenuDate",
                table: "ExtraItem");

            migrationBuilder.AlterColumn<long>(
                name: "SetMenuId",
                table: "ExtraItem",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
