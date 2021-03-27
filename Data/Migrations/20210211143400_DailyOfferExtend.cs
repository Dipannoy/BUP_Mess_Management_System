using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class DailyOfferExtend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "DailyOfferItem",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DailyOfferItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "OrderLimit",
                table: "DailyOfferItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "DailyOfferItem");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DailyOfferItem");

            migrationBuilder.DropColumn(
                name: "OrderLimit",
                table: "DailyOfferItem");
        }
    }
}
