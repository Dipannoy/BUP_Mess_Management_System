using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class ExtraChitParent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumerMealWiseExtrachit_MealType_MealTypeId",
                table: "ConsumerMealWiseExtrachit");

            migrationBuilder.DropForeignKey(
                name: "FK_ConsumerMealWiseExtrachit_OrderType_OrderTypeId",
                table: "ConsumerMealWiseExtrachit");

            migrationBuilder.DropForeignKey(
                name: "FK_ConsumerMealWiseExtrachit_AspNetUsers_UserId",
                table: "ConsumerMealWiseExtrachit");

            migrationBuilder.DropIndex(
                name: "IX_ConsumerMealWiseExtrachit_MealTypeId",
                table: "ConsumerMealWiseExtrachit");

            migrationBuilder.DropIndex(
                name: "IX_ConsumerMealWiseExtrachit_UserId",
                table: "ConsumerMealWiseExtrachit");

            migrationBuilder.DropColumn(
                name: "Attribute1",
                table: "ConsumerMealWiseExtrachit");

            migrationBuilder.DropColumn(
                name: "Attribute2",
                table: "ConsumerMealWiseExtrachit");

            migrationBuilder.DropColumn(
                name: "Attribute3",
                table: "ConsumerMealWiseExtrachit");

            migrationBuilder.DropColumn(
                name: "MealTypeId",
                table: "ConsumerMealWiseExtrachit");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "ConsumerMealWiseExtrachit");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ConsumerMealWiseExtrachit");

            migrationBuilder.RenameColumn(
                name: "OrderTypeId",
                table: "ConsumerMealWiseExtrachit",
                newName: "ConsumerMealWiseExtraChitParentId");

            migrationBuilder.RenameIndex(
                name: "IX_ConsumerMealWiseExtrachit_OrderTypeId",
                table: "ConsumerMealWiseExtrachit",
                newName: "IX_ConsumerMealWiseExtrachit_ConsumerMealWiseExtraChitParentId");

            migrationBuilder.CreateTable(
                name: "ConsumerMealWiseExtraChitParent",
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
                    Remarks = table.Column<string>(nullable: true),
                    Attribute1 = table.Column<string>(nullable: true),
                    Attribute2 = table.Column<string>(nullable: true),
                    Attribute3 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumerMealWiseExtraChitParent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumerMealWiseExtraChitParent_MealType_MealTypeId",
                        column: x => x.MealTypeId,
                        principalTable: "MealType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsumerMealWiseExtraChitParent_OrderType_OrderTypeId",
                        column: x => x.OrderTypeId,
                        principalTable: "OrderType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsumerMealWiseExtraChitParent_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerMealWiseExtraChitParent_MealTypeId",
                table: "ConsumerMealWiseExtraChitParent",
                column: "MealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerMealWiseExtraChitParent_OrderTypeId",
                table: "ConsumerMealWiseExtraChitParent",
                column: "OrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerMealWiseExtraChitParent_UserId",
                table: "ConsumerMealWiseExtraChitParent",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumerMealWiseExtrachit_ConsumerMealWiseExtraChitParent_ConsumerMealWiseExtraChitParentId",
                table: "ConsumerMealWiseExtrachit",
                column: "ConsumerMealWiseExtraChitParentId",
                principalTable: "ConsumerMealWiseExtraChitParent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumerMealWiseExtrachit_ConsumerMealWiseExtraChitParent_ConsumerMealWiseExtraChitParentId",
                table: "ConsumerMealWiseExtrachit");

            migrationBuilder.DropTable(
                name: "ConsumerMealWiseExtraChitParent");

            migrationBuilder.RenameColumn(
                name: "ConsumerMealWiseExtraChitParentId",
                table: "ConsumerMealWiseExtrachit",
                newName: "OrderTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ConsumerMealWiseExtrachit_ConsumerMealWiseExtraChitParentId",
                table: "ConsumerMealWiseExtrachit",
                newName: "IX_ConsumerMealWiseExtrachit_OrderTypeId");

            migrationBuilder.AddColumn<string>(
                name: "Attribute1",
                table: "ConsumerMealWiseExtrachit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Attribute2",
                table: "ConsumerMealWiseExtrachit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Attribute3",
                table: "ConsumerMealWiseExtrachit",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MealTypeId",
                table: "ConsumerMealWiseExtrachit",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "ConsumerMealWiseExtrachit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ConsumerMealWiseExtrachit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerMealWiseExtrachit_MealTypeId",
                table: "ConsumerMealWiseExtrachit",
                column: "MealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerMealWiseExtrachit_UserId",
                table: "ConsumerMealWiseExtrachit",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumerMealWiseExtrachit_MealType_MealTypeId",
                table: "ConsumerMealWiseExtrachit",
                column: "MealTypeId",
                principalTable: "MealType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumerMealWiseExtrachit_OrderType_OrderTypeId",
                table: "ConsumerMealWiseExtrachit",
                column: "OrderTypeId",
                principalTable: "OrderType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumerMealWiseExtrachit_AspNetUsers_UserId",
                table: "ConsumerMealWiseExtrachit",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
