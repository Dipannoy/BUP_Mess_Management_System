using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class lt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "OrderHistoryVr2",
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
                    SetMenuId = table.Column<long>(nullable: true),
                    MealTypeId = table.Column<long>(nullable: false),
                    StoreOutItemId = table.Column<long>(nullable: true),
                    OrderTypeId = table.Column<long>(nullable: true),
                    UnitOrdered = table.Column<double>(nullable: false),
                    OrderAmount = table.Column<double>(nullable: false),
                    IsPreOrder = table.Column<bool>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    IsForwardedToOffice = table.Column<bool>(nullable: true),
                    ForwardRemarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistoryVr2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHistoryVr2_MealType_MealTypeId",
                        column: x => x.MealTypeId,
                        principalTable: "MealType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderHistoryVr2_OrderType_OrderTypeId",
                        column: x => x.OrderTypeId,
                        principalTable: "OrderType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderHistoryVr2_SetMenu_SetMenuId",
                        column: x => x.SetMenuId,
                        principalTable: "SetMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderHistoryVr2_StoreOutItem_StoreOutItemId",
                        column: x => x.StoreOutItemId,
                        principalTable: "StoreOutItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderHistoryVr2_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistoryVr2_MealTypeId",
                table: "OrderHistoryVr2",
                column: "MealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistoryVr2_OrderTypeId",
                table: "OrderHistoryVr2",
                column: "OrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistoryVr2_SetMenuId",
                table: "OrderHistoryVr2",
                column: "SetMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistoryVr2_StoreOutItemId",
                table: "OrderHistoryVr2",
                column: "StoreOutItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistoryVr2_UserId",
                table: "OrderHistoryVr2",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderHistoryVr2");

            migrationBuilder.CreateTable(
                name: "OrderHistoryV2",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ForwardRemarks = table.Column<string>(nullable: true),
                    IsForwardedToOffice = table.Column<bool>(nullable: true),
                    IsPreOrder = table.Column<bool>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    MealTypeId = table.Column<long>(nullable: false),
                    OrderAmount = table.Column<double>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    OrderTypeId = table.Column<long>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SetMenuId = table.Column<long>(nullable: true),
                    StoreOutItemId = table.Column<long>(nullable: true),
                    UnitOrdered = table.Column<double>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistoryV2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHistoryV2_MealType_MealTypeId",
                        column: x => x.MealTypeId,
                        principalTable: "MealType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderHistoryV2_OrderType_OrderTypeId",
                        column: x => x.OrderTypeId,
                        principalTable: "OrderType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderHistoryV2_SetMenu_SetMenuId",
                        column: x => x.SetMenuId,
                        principalTable: "SetMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderHistoryV2_StoreOutItem_StoreOutItemId",
                        column: x => x.StoreOutItemId,
                        principalTable: "StoreOutItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderHistoryV2_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistoryV2_MealTypeId",
                table: "OrderHistoryV2",
                column: "MealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistoryV2_OrderTypeId",
                table: "OrderHistoryV2",
                column: "OrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistoryV2_SetMenuId",
                table: "OrderHistoryV2",
                column: "SetMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistoryV2_StoreOutItemId",
                table: "OrderHistoryV2",
                column: "StoreOutItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistoryV2_UserId",
                table: "OrderHistoryV2",
                column: "UserId");
        }
    }
}
