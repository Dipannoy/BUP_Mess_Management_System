using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class AddedFieldInWarehouseAndRemainingBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PurchaseRate",
                table: "WarehouseStorage",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalPurchasePrice",
                table: "WarehouseStorage",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "RemainingBalanceAndWeightedPriceCalculation",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseRate",
                table: "WarehouseStorage");

            migrationBuilder.DropColumn(
                name: "TotalPurchasePrice",
                table: "WarehouseStorage");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "RemainingBalanceAndWeightedPriceCalculation");
        }
    }
}
