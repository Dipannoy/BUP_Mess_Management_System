using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class sa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerChoiceV2",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    SetMenuId = table.Column<long>(nullable: true),
                    ExtraItemId = table.Column<long>(nullable: true),
                    OrderTypeId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    MealTypeId = table.Column<long>(nullable: false),
                    quantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerChoiceV2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerChoiceV2_ExtraItem_ExtraItemId",
                        column: x => x.ExtraItemId,
                        principalTable: "ExtraItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerChoiceV2_MealType_MealTypeId",
                        column: x => x.MealTypeId,
                        principalTable: "MealType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerChoiceV2_OrderType_OrderTypeId",
                        column: x => x.OrderTypeId,
                        principalTable: "OrderType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerChoiceV2_SetMenu_SetMenuId",
                        column: x => x.SetMenuId,
                        principalTable: "SetMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerChoiceV2_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerChoiceV2_ExtraItemId",
                table: "CustomerChoiceV2",
                column: "ExtraItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerChoiceV2_MealTypeId",
                table: "CustomerChoiceV2",
                column: "MealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerChoiceV2_OrderTypeId",
                table: "CustomerChoiceV2",
                column: "OrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerChoiceV2_SetMenuId",
                table: "CustomerChoiceV2",
                column: "SetMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerChoiceV2_UserId",
                table: "CustomerChoiceV2",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerChoiceV2");
        }
    }
}
