using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class recipemodelcorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequiredStoreOutUnit",
                table: "StoreOutItemRecipe",
                newName: "RequiredStoreInUnit");

            migrationBuilder.RenameColumn(
                name: "MinimumStoreInUnit",
                table: "StoreOutItemRecipe",
                newName: "MinimumStoreOutUnit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequiredStoreInUnit",
                table: "StoreOutItemRecipe",
                newName: "RequiredStoreOutUnit");

            migrationBuilder.RenameColumn(
                name: "MinimumStoreOutUnit",
                table: "StoreOutItemRecipe",
                newName: "MinimumStoreInUnit");
        }
    }
}
