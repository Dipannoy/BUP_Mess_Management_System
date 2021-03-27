using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class CustomerChoiceOnSpot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OnSpotParentId",
                table: "CustomerChoiceV2",
                nullable: true,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerChoiceV2_OnSpotParentId",
                table: "CustomerChoiceV2",
                column: "OnSpotParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerChoiceV2_OnSpotParent_OnSpotParentId",
                table: "CustomerChoiceV2",
                column: "OnSpotParentId",
                principalTable: "OnSpotParent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerChoiceV2_OnSpotParent_OnSpotParentId",
                table: "CustomerChoiceV2");

            migrationBuilder.DropIndex(
                name: "IX_CustomerChoiceV2_OnSpotParentId",
                table: "CustomerChoiceV2");

            migrationBuilder.DropColumn(
                name: "OnSpotParentId",
                table: "CustomerChoiceV2");
        }
    }
}
