using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class ConsumerMealExtraChit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsumerMealWiseExtrachit",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    OrderTypeId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    MealTypeId = table.Column<long>(nullable: false),
                    StoreOutItemId = table.Column<long>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    Attribute1 = table.Column<string>(nullable: true),
                    Attribute2 = table.Column<string>(nullable: true),
                    Attribute3 = table.Column<string>(nullable: true),
                    quantity = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumerMealWiseExtrachit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumerMealWiseExtrachit_MealType_MealTypeId",
                        column: x => x.MealTypeId,
                        principalTable: "MealType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsumerMealWiseExtrachit_OrderType_OrderTypeId",
                        column: x => x.OrderTypeId,
                        principalTable: "OrderType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsumerMealWiseExtrachit_StoreOutItem_StoreOutItemId",
                        column: x => x.StoreOutItemId,
                        principalTable: "StoreOutItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsumerMealWiseExtrachit_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerMealWiseExtrachit_MealTypeId",
                table: "ConsumerMealWiseExtrachit",
                column: "MealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerMealWiseExtrachit_OrderTypeId",
                table: "ConsumerMealWiseExtrachit",
                column: "OrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerMealWiseExtrachit_StoreOutItemId",
                table: "ConsumerMealWiseExtrachit",
                column: "StoreOutItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerMealWiseExtrachit_UserId",
                table: "ConsumerMealWiseExtrachit",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumerMealWiseExtrachit");
        }
    }
}
