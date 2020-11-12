using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class AddSetMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPreOrderSet",
                table: "PreOrderSchedule",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastConfigurationUpdateDate",
                table: "PreOrderSchedule",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "MealTypeId",
                table: "PreOrderSchedule",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PreOrderSchedule",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPreOrder",
                table: "OrderHistory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "MealTypeId",
                table: "OrderHistory",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "OrderAmount",
                table: "OrderHistory",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "OrderHistory",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "SetMenuId",
                table: "OrderHistory",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StoreOutItemId",
                table: "OrderHistory",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<double>(
                name: "UnitOrdered",
                table: "OrderHistory",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "OrderHistory",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MealType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Serial = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsAvailableForPreOrder = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SetMenu",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SetMenuDate = table.Column<DateTime>(nullable: false),
                    Serial = table.Column<int>(nullable: false),
                    IsAvailableAsExtra = table.Column<bool>(nullable: false),
                    MealTypeId = table.Column<long>(nullable: true),
                    StoreOutItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SetMenu_MealType_MealTypeId",
                        column: x => x.MealTypeId,
                        principalTable: "MealType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SetMenu_StoreOutItem_StoreOutItemId",
                        column: x => x.StoreOutItemId,
                        principalTable: "StoreOutItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreOrderSchedule_MealTypeId",
                table: "PreOrderSchedule",
                column: "MealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PreOrderSchedule_UserId",
                table: "PreOrderSchedule",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_MealTypeId",
                table: "OrderHistory",
                column: "MealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_SetMenuId",
                table: "OrderHistory",
                column: "SetMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_StoreOutItemId",
                table: "OrderHistory",
                column: "StoreOutItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_UserId",
                table: "OrderHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SetMenu_MealTypeId",
                table: "SetMenu",
                column: "MealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SetMenu_StoreOutItemId",
                table: "SetMenu",
                column: "StoreOutItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistory_MealType_MealTypeId",
                table: "OrderHistory",
                column: "MealTypeId",
                principalTable: "MealType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistory_SetMenu_SetMenuId",
                table: "OrderHistory",
                column: "SetMenuId",
                principalTable: "SetMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistory_StoreOutItem_StoreOutItemId",
                table: "OrderHistory",
                column: "StoreOutItemId",
                principalTable: "StoreOutItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistory_AspNetUsers_UserId",
                table: "OrderHistory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreOrderSchedule_MealType_MealTypeId",
                table: "PreOrderSchedule",
                column: "MealTypeId",
                principalTable: "MealType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreOrderSchedule_AspNetUsers_UserId",
                table: "PreOrderSchedule",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistory_MealType_MealTypeId",
                table: "OrderHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistory_SetMenu_SetMenuId",
                table: "OrderHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistory_StoreOutItem_StoreOutItemId",
                table: "OrderHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistory_AspNetUsers_UserId",
                table: "OrderHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_PreOrderSchedule_MealType_MealTypeId",
                table: "PreOrderSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_PreOrderSchedule_AspNetUsers_UserId",
                table: "PreOrderSchedule");

            migrationBuilder.DropTable(
                name: "SetMenu");

            migrationBuilder.DropTable(
                name: "MealType");

            migrationBuilder.DropIndex(
                name: "IX_PreOrderSchedule_MealTypeId",
                table: "PreOrderSchedule");

            migrationBuilder.DropIndex(
                name: "IX_PreOrderSchedule_UserId",
                table: "PreOrderSchedule");

            migrationBuilder.DropIndex(
                name: "IX_OrderHistory_MealTypeId",
                table: "OrderHistory");

            migrationBuilder.DropIndex(
                name: "IX_OrderHistory_SetMenuId",
                table: "OrderHistory");

            migrationBuilder.DropIndex(
                name: "IX_OrderHistory_StoreOutItemId",
                table: "OrderHistory");

            migrationBuilder.DropIndex(
                name: "IX_OrderHistory_UserId",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "IsPreOrderSet",
                table: "PreOrderSchedule");

            migrationBuilder.DropColumn(
                name: "LastConfigurationUpdateDate",
                table: "PreOrderSchedule");

            migrationBuilder.DropColumn(
                name: "MealTypeId",
                table: "PreOrderSchedule");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PreOrderSchedule");

            migrationBuilder.DropColumn(
                name: "IsPreOrder",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "MealTypeId",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "OrderAmount",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "SetMenuId",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "StoreOutItemId",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "UnitOrdered",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrderHistory");
        }
    }
}
