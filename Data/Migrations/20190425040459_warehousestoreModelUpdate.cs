using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class warehousestoreModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistory_StoreOutItem_StoreOutItemId",
                table: "OrderHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_PreOrderSchedule_MealType_MealTypeId",
                table: "PreOrderSchedule");

            migrationBuilder.CreateTable(
                name: "RemainingBalanceAndWeightedPriceCalculation",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TotalAvailableAmount = table.Column<double>(nullable: false),
                    WeightedPrice = table.Column<double>(nullable: false),
                    StoreInCategoryId = table.Column<long>(nullable: false),
                    StoreInItemId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemainingBalanceAndWeightedPriceCalculation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RemainingBalanceAndWeightedPriceCalculation_StoreInItemCategory_StoreInCategoryId",
                        column: x => x.StoreInCategoryId,
                        principalTable: "StoreInItemCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RemainingBalanceAndWeightedPriceCalculation_StoreInItem_StoreInItemId",
                        column: x => x.StoreInItemId,
                        principalTable: "StoreInItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseStorage",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    IsStoreOut = table.Column<bool>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    StoreInItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseStorage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseStorage_StoreInItem_StoreInItemId",
                        column: x => x.StoreInItemId,
                        principalTable: "StoreInItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RemainingBalanceAndWeightedPriceCalculation_StoreInCategoryId",
                table: "RemainingBalanceAndWeightedPriceCalculation",
                column: "StoreInCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RemainingBalanceAndWeightedPriceCalculation_StoreInItemId",
                table: "RemainingBalanceAndWeightedPriceCalculation",
                column: "StoreInItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseStorage_StoreInItemId",
                table: "WarehouseStorage",
                column: "StoreInItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistory_StoreOutItem_StoreOutItemId",
                table: "OrderHistory",
                column: "StoreOutItemId",
                principalTable: "StoreOutItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreOrderSchedule_MealType_MealTypeId",
                table: "PreOrderSchedule",
                column: "MealTypeId",
                principalTable: "MealType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistory_StoreOutItem_StoreOutItemId",
                table: "OrderHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_PreOrderSchedule_MealType_MealTypeId",
                table: "PreOrderSchedule");

            migrationBuilder.DropTable(
                name: "RemainingBalanceAndWeightedPriceCalculation");

            migrationBuilder.DropTable(
                name: "WarehouseStorage");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistory_StoreOutItem_StoreOutItemId",
                table: "OrderHistory",
                column: "StoreOutItemId",
                principalTable: "StoreOutItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreOrderSchedule_MealType_MealTypeId",
                table: "PreOrderSchedule",
                column: "MealTypeId",
                principalTable: "MealType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
