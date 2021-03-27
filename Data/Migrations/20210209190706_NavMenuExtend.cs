using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class NavMenuExtend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RouteVariable",
                table: "NavigationMenu",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RouteVariableValue",
                table: "NavigationMenu",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RouteVariable",
                table: "NavigationMenu");

            migrationBuilder.DropColumn(
                name: "RouteVariableValue",
                table: "NavigationMenu");
        }
    }
}
