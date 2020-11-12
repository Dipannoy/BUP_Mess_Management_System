using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class storemodelsadd4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreInItem_StoreInItemCategory_StoreInCategoryId",
                table: "StoreInItem");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreOutItem_StoreOutItemCategory_StoreOutCategoryId",
                table: "StoreOutItem");

            migrationBuilder.CreateTable(
                name: "StoreOutItemRecipe",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    StoreOutItemId = table.Column<long>(nullable: false),
                    StoreInItemId = table.Column<long>(nullable: false),
                    MinimumStoreInUnit = table.Column<double>(nullable: false),
                    RequiredStoreOutUnit = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreOutItemRecipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreOutItemRecipe_StoreInItem_StoreInItemId",
                        column: x => x.StoreInItemId,
                        principalTable: "StoreInItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreOutItemRecipe_StoreOutItem_StoreOutItemId",
                        column: x => x.StoreOutItemId,
                        principalTable: "StoreOutItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreOutItemRecipe_StoreInItemId",
                table: "StoreOutItemRecipe",
                column: "StoreInItemId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreOutItemRecipe_StoreOutItemId",
                table: "StoreOutItemRecipe",
                column: "StoreOutItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreInItem_StoreInItemCategory_StoreInCategoryId",
                table: "StoreInItem",
                column: "StoreInCategoryId",
                principalTable: "StoreInItemCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreOutItem_StoreOutItemCategory_StoreOutCategoryId",
                table: "StoreOutItem",
                column: "StoreOutCategoryId",
                principalTable: "StoreOutItemCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreInItem_StoreInItemCategory_StoreInCategoryId",
                table: "StoreInItem");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreOutItem_StoreOutItemCategory_StoreOutCategoryId",
                table: "StoreOutItem");

            migrationBuilder.DropTable(
                name: "StoreOutItemRecipe");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreInItem_StoreInItemCategory_StoreInCategoryId",
                table: "StoreInItem",
                column: "StoreInCategoryId",
                principalTable: "StoreInItemCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreOutItem_StoreOutItemCategory_StoreOutCategoryId",
                table: "StoreOutItem",
                column: "StoreOutCategoryId",
                principalTable: "StoreOutItemCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
