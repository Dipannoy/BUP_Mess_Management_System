using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class ConsumerAttachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsumerPaymentAttachment",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ConsumerBillParentId = table.Column<long>(nullable: false),
                    UploadDate = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    Attachment = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: true),
                    EntryDone = table.Column<bool>(nullable: false),
                    Attribute1 = table.Column<string>(nullable: true),
                    Attribute2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumerPaymentAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumerPaymentAttachment_ConsumerBillParent_ConsumerBillParentId",
                        column: x => x.ConsumerBillParentId,
                        principalTable: "ConsumerBillParent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerPaymentAttachment_ConsumerBillParentId",
                table: "ConsumerPaymentAttachment",
                column: "ConsumerBillParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumerPaymentAttachment");
        }
    }
}
