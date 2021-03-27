using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class AttachmentExtend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ConsumerPaymentInfoId",
                table: "ConsumerPaymentAttachment",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerPaymentAttachment_ConsumerPaymentInfoId",
                table: "ConsumerPaymentAttachment",
                column: "ConsumerPaymentInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumerPaymentAttachment_ConsumerPaymentInfo_ConsumerPaymentInfoId",
                table: "ConsumerPaymentAttachment",
                column: "ConsumerPaymentInfoId",
                principalTable: "ConsumerPaymentInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumerPaymentAttachment_ConsumerPaymentInfo_ConsumerPaymentInfoId",
                table: "ConsumerPaymentAttachment");

            migrationBuilder.DropIndex(
                name: "IX_ConsumerPaymentAttachment_ConsumerPaymentInfoId",
                table: "ConsumerPaymentAttachment");

            migrationBuilder.DropColumn(
                name: "ConsumerPaymentInfoId",
                table: "ConsumerPaymentAttachment");
        }
    }
}
