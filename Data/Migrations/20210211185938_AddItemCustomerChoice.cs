using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class AddItemCustomerChoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StoreOutItemId",
                table: "CustomerChoiceV2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerChoiceV2_StoreOutItemId",
                table: "CustomerChoiceV2",
                column: "StoreOutItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerChoiceV2_StoreOutItem_StoreOutItemId",
                table: "CustomerChoiceV2",
                column: "StoreOutItemId",
                principalTable: "StoreOutItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerChoiceV2_StoreOutItem_StoreOutItemId",
                table: "CustomerChoiceV2");

            migrationBuilder.DropIndex(
                name: "IX_CustomerChoiceV2_StoreOutItemId",
                table: "CustomerChoiceV2");

            migrationBuilder.DropColumn(
                name: "StoreOutItemId",
                table: "CustomerChoiceV2");
        }
    }
}
