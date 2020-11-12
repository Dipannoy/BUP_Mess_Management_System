using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class SpclMod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecialMenuOrder_MealType_MealTypeId",
                table: "SpecialMenuOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialMenuOrder_Office_OfficeId",
                table: "SpecialMenuOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialMenuOrder_AspNetUsers_UserId",
                table: "SpecialMenuOrder");

            migrationBuilder.DropIndex(
                name: "IX_SpecialMenuOrder_OfficeId",
                table: "SpecialMenuOrder");

            migrationBuilder.DropIndex(
                name: "IX_SpecialMenuOrder_UserId",
                table: "SpecialMenuOrder");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "SpecialMenuOrder");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "SpecialMenuOrder");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SpecialMenuOrder");

            migrationBuilder.RenameColumn(
                name: "MealTypeId",
                table: "SpecialMenuOrder",
                newName: "SpecialMenuParentId");

            migrationBuilder.RenameIndex(
                name: "IX_SpecialMenuOrder_MealTypeId",
                table: "SpecialMenuOrder",
                newName: "IX_SpecialMenuOrder_SpecialMenuParentId");

            migrationBuilder.CreateTable(
                name: "SpecialMenuParent",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    MealTypeId = table.Column<long>(nullable: false),
                    OfficeId = table.Column<long>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialMenuParent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialMenuParent_MealType_MealTypeId",
                        column: x => x.MealTypeId,
                        principalTable: "MealType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpecialMenuParent_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpecialMenuParent_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpecialMenuParent_MealTypeId",
                table: "SpecialMenuParent",
                column: "MealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialMenuParent_OfficeId",
                table: "SpecialMenuParent",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialMenuParent_UserId",
                table: "SpecialMenuParent",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialMenuOrder_SpecialMenuParent_SpecialMenuParentId",
                table: "SpecialMenuOrder",
                column: "SpecialMenuParentId",
                principalTable: "SpecialMenuParent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecialMenuOrder_SpecialMenuParent_SpecialMenuParentId",
                table: "SpecialMenuOrder");

            migrationBuilder.DropTable(
                name: "SpecialMenuParent");

            migrationBuilder.RenameColumn(
                name: "SpecialMenuParentId",
                table: "SpecialMenuOrder",
                newName: "MealTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_SpecialMenuOrder_SpecialMenuParentId",
                table: "SpecialMenuOrder",
                newName: "IX_SpecialMenuOrder_MealTypeId");

            migrationBuilder.AddColumn<long>(
                name: "OfficeId",
                table: "SpecialMenuOrder",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "SpecialMenuOrder",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SpecialMenuOrder",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpecialMenuOrder_OfficeId",
                table: "SpecialMenuOrder",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialMenuOrder_UserId",
                table: "SpecialMenuOrder",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialMenuOrder_MealType_MealTypeId",
                table: "SpecialMenuOrder",
                column: "MealTypeId",
                principalTable: "MealType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialMenuOrder_Office_OfficeId",
                table: "SpecialMenuOrder",
                column: "OfficeId",
                principalTable: "Office",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialMenuOrder_AspNetUsers_UserId",
                table: "SpecialMenuOrder",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
