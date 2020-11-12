using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class AddWarehouseStorageRemainingBalanceAndWeightedPriceCalculation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RemainingBalanceAndWeightedPriceCalculation_StoreInItemCategory_StoreInCategoryId",
                table: "RemainingBalanceAndWeightedPriceCalculation");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreInItem_UnitType_UnitTypeId",
                table: "StoreInItem");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseStorage_StoreInItem_StoreInItemId",
                table: "WarehouseStorage");

            migrationBuilder.DropIndex(
                name: "IX_RemainingBalanceAndWeightedPriceCalculation_StoreInCategoryId",
                table: "RemainingBalanceAndWeightedPriceCalculation");

            migrationBuilder.DropColumn(
                name: "StoreInCategoryId",
                table: "RemainingBalanceAndWeightedPriceCalculation");

            migrationBuilder.AlterColumn<long>(
                name: "StoreInItemId",
                table: "RemainingBalanceAndWeightedPriceCalculation",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreInItem_UnitType_UnitTypeId",
                table: "StoreInItem",
                column: "UnitTypeId",
                principalTable: "UnitType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseStorage_StoreInItem_StoreInItemId",
                table: "WarehouseStorage",
                column: "StoreInItemId",
                principalTable: "StoreInItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreInItem_UnitType_UnitTypeId",
                table: "StoreInItem");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseStorage_StoreInItem_StoreInItemId",
                table: "WarehouseStorage");

            migrationBuilder.AlterColumn<long>(
                name: "StoreInItemId",
                table: "RemainingBalanceAndWeightedPriceCalculation",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "StoreInCategoryId",
                table: "RemainingBalanceAndWeightedPriceCalculation",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_RemainingBalanceAndWeightedPriceCalculation_StoreInCategoryId",
                table: "RemainingBalanceAndWeightedPriceCalculation",
                column: "StoreInCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_RemainingBalanceAndWeightedPriceCalculation_StoreInItemCategory_StoreInCategoryId",
                table: "RemainingBalanceAndWeightedPriceCalculation",
                column: "StoreInCategoryId",
                principalTable: "StoreInItemCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreInItem_UnitType_UnitTypeId",
                table: "StoreInItem",
                column: "UnitTypeId",
                principalTable: "UnitType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseStorage_StoreInItem_StoreInItemId",
                table: "WarehouseStorage",
                column: "StoreInItemId",
                principalTable: "StoreInItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
