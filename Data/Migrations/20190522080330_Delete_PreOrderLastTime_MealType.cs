using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class Delete_PreOrderLastTime_MealType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreOrderLastTime",
                table: "MealType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PreOrderLastTime",
                table: "MealType",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
