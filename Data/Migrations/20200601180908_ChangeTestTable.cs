using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class ChangeTestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameTable(name: "TestClass", newName: "TestClassMod");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameTable(name: "TestClassMod", newName: "TestClass");
         
        }
    }
}
