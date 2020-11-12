using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class Attributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ForwardRemarks",
                table: "OrderHistory",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsForwardedToOffice",
                table: "OrderHistory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForwardRemarks",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "IsForwardedToOffice",
                table: "OrderHistory");
        }
    }
}
