using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class DailySetMenuCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailySetMenu",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SetMenuDate = table.Column<DateTime>(nullable: true),
                    Day = table.Column<int>(nullable: false),
                    MealTypeId = table.Column<long>(nullable: false),
                    SetMenuId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailySetMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailySetMenu_MealType_MealTypeId",
                        column: x => x.MealTypeId,
                        principalTable: "MealType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailySetMenu_SetMenu_SetMenuId",
                        column: x => x.SetMenuId,
                        principalTable: "SetMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailySetMenu_MealTypeId",
                table: "DailySetMenu",
                column: "MealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DailySetMenu_SetMenuId",
                table: "DailySetMenu",
                column: "SetMenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailySetMenu");
        }
    }
}
