using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class AddSetMenuDetails_ExteaItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetMenu_MealType_MealTypeId",
                table: "SetMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_SetMenu_StoreOutItem_StoreOutItemId",
                table: "SetMenu");

            migrationBuilder.DropIndex(
                name: "IX_SetMenu_MealTypeId",
                table: "SetMenu");

            migrationBuilder.DropIndex(
                name: "IX_SetMenu_StoreOutItemId",
                table: "SetMenu");

            migrationBuilder.DropColumn(
                name: "MealTypeId",
                table: "SetMenu");

            migrationBuilder.DropColumn(
                name: "StoreOutItemId",
                table: "SetMenu");

            migrationBuilder.CreateTable(
                name: "ExtraItem",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    SetMenuId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtraItem_SetMenu_SetMenuId",
                        column: x => x.SetMenuId,
                        principalTable: "SetMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SetMenuDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SetMenuId = table.Column<long>(nullable: false),
                    MealTypeId = table.Column<long>(nullable: false),
                    StoreOutItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetMenuDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SetMenuDetails_MealType_MealTypeId",
                        column: x => x.MealTypeId,
                        principalTable: "MealType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SetMenuDetails_SetMenu_SetMenuId",
                        column: x => x.SetMenuId,
                        principalTable: "SetMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SetMenuDetails_StoreOutItem_StoreOutItemId",
                        column: x => x.StoreOutItemId,
                        principalTable: "StoreOutItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtraItem_SetMenuId",
                table: "ExtraItem",
                column: "SetMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_SetMenuDetails_MealTypeId",
                table: "SetMenuDetails",
                column: "MealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SetMenuDetails_SetMenuId",
                table: "SetMenuDetails",
                column: "SetMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_SetMenuDetails_StoreOutItemId",
                table: "SetMenuDetails",
                column: "StoreOutItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtraItem");

            migrationBuilder.DropTable(
                name: "SetMenuDetails");

            migrationBuilder.AddColumn<long>(
                name: "MealTypeId",
                table: "SetMenu",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StoreOutItemId",
                table: "SetMenu",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_SetMenu_MealTypeId",
                table: "SetMenu",
                column: "MealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SetMenu_StoreOutItemId",
                table: "SetMenu",
                column: "StoreOutItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_SetMenu_MealType_MealTypeId",
                table: "SetMenu",
                column: "MealTypeId",
                principalTable: "MealType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SetMenu_StoreOutItem_StoreOutItemId",
                table: "SetMenu",
                column: "StoreOutItemId",
                principalTable: "StoreOutItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
