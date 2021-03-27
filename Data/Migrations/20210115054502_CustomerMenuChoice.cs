using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class CustomerMenuChoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StoreOutItemId",
                table: "MenuItem",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerDailyMenuChoice",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    quantity = table.Column<double>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    MenuItemId = table.Column<long>(nullable: true),
                    OrderTypeId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    MealTypeId = table.Column<long>(nullable: false),
                    ExtraItemId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDailyMenuChoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerDailyMenuChoice_ExtraItem_ExtraItemId",
                        column: x => x.ExtraItemId,
                        principalTable: "ExtraItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerDailyMenuChoice_MealType_MealTypeId",
                        column: x => x.MealTypeId,
                        principalTable: "MealType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerDailyMenuChoice_MenuItem_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerDailyMenuChoice_OrderType_OrderTypeId",
                        column: x => x.OrderTypeId,
                        principalTable: "OrderType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerDailyMenuChoice_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItem_StoreOutItemId",
                table: "MenuItem",
                column: "StoreOutItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDailyMenuChoice_ExtraItemId",
                table: "CustomerDailyMenuChoice",
                column: "ExtraItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDailyMenuChoice_MealTypeId",
                table: "CustomerDailyMenuChoice",
                column: "MealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDailyMenuChoice_MenuItemId",
                table: "CustomerDailyMenuChoice",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDailyMenuChoice_OrderTypeId",
                table: "CustomerDailyMenuChoice",
                column: "OrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDailyMenuChoice_UserId",
                table: "CustomerDailyMenuChoice",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_StoreOutItem_StoreOutItemId",
                table: "MenuItem",
                column: "StoreOutItemId",
                principalTable: "StoreOutItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_StoreOutItem_StoreOutItemId",
                table: "MenuItem");

            migrationBuilder.DropTable(
                name: "CustomerDailyMenuChoice");

            migrationBuilder.DropIndex(
                name: "IX_MenuItem_StoreOutItemId",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "StoreOutItemId",
                table: "MenuItem");
        }
    }
}
