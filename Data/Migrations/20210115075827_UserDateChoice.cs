using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class UserDateChoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDateChoiceMaster",
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
                    MealTypeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDateChoiceMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDateChoiceMaster_MealType_MealTypeId",
                        column: x => x.MealTypeId,
                        principalTable: "MealType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserDateChoiceMaster_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserDateChoiceDetail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UserDateChoiceMasterId = table.Column<long>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    IsOrderSet = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDateChoiceDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDateChoiceDetail_UserDateChoiceMaster_UserDateChoiceMasterId",
                        column: x => x.UserDateChoiceMasterId,
                        principalTable: "UserDateChoiceMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDateChoiceDetail_UserDateChoiceMasterId",
                table: "UserDateChoiceDetail",
                column: "UserDateChoiceMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDateChoiceMaster_MealTypeId",
                table: "UserDateChoiceMaster",
                column: "MealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDateChoiceMaster_UserId",
                table: "UserDateChoiceMaster",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDateChoiceDetail");

            migrationBuilder.DropTable(
                name: "UserDateChoiceMaster");
        }
    }
}
