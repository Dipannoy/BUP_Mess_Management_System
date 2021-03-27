using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class RemoveTrans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionID",
                table: "ConsumerBillHistory");

            migrationBuilder.AddColumn<string>(
                name: "TransactionID",
                table: "ConsumerPaymentInfo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionID",
                table: "ConsumerPaymentInfo");

            migrationBuilder.AddColumn<string>(
                name: "TransactionID",
                table: "ConsumerBillHistory",
                nullable: true);
        }
    }
}
