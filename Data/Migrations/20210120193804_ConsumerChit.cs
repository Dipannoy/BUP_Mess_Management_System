using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class ConsumerChit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ExtraChitParentId",
                table: "CustomerChoiceV2",
                nullable: true,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerChoiceV2_ExtraChitParentId",
                table: "CustomerChoiceV2",
                column: "ExtraChitParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerChoiceV2_ExtraChitParent_ExtraChitParentId",
                table: "CustomerChoiceV2",
                column: "ExtraChitParentId",
                principalTable: "ExtraChitParent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerChoiceV2_ExtraChitParent_ExtraChitParentId",
                table: "CustomerChoiceV2");

            migrationBuilder.DropIndex(
                name: "IX_CustomerChoiceV2_ExtraChitParentId",
                table: "CustomerChoiceV2");

            migrationBuilder.DropColumn(
                name: "ExtraChitParentId",
                table: "CustomerChoiceV2");
        }
    }
}
