using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class UpdateSetMenuIsActiveAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "SubMenu",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "SubMenu");
        }
    }
}
