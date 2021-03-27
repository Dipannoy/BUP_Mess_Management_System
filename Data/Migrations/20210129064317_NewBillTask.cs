using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mess_Management_System_Alpha_V2.Data.Migrations
{
    public partial class NewBillTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsumerBillParent",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Attribute1 = table.Column<string>(nullable: true),
                    Attribute2 = table.Column<string>(nullable: true),
                    Attribute3 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumerBillParent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumerBillParent_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    MethodName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsumerPaymentInfo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    PaymentMethodId = table.Column<long>(nullable: false),
                    ConsumerBillParentId = table.Column<long>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Attribute1 = table.Column<string>(nullable: true),
                    Attribute2 = table.Column<string>(nullable: true),
                    Attribute3 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumerPaymentInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumerPaymentInfo_ConsumerBillParent_ConsumerBillParentId",
                        column: x => x.ConsumerBillParentId,
                        principalTable: "ConsumerBillParent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsumerPaymentInfo_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConsumerBillHistory",
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
                    ConsumerPaymentInfoId = table.Column<long>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    PaymentAmount = table.Column<double>(nullable: false),
                    Due = table.Column<double>(nullable: false),
                    IsPartial = table.Column<bool>(nullable: false),
                    TransactionID = table.Column<string>(nullable: true),
                    ReceivedBy = table.Column<string>(nullable: true),
                    Attribute1 = table.Column<string>(nullable: true),
                    Attribute2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumerBillHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumerBillHistory_ConsumerBillParent_ConsumerBillParentId",
                        column: x => x.ConsumerBillParentId,
                        principalTable: "ConsumerBillParent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsumerBillHistory_ConsumerPaymentInfo_ConsumerPaymentInfoId",
                        column: x => x.ConsumerPaymentInfoId,
                        principalTable: "ConsumerPaymentInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerBillHistory_ConsumerBillParentId",
                table: "ConsumerBillHistory",
                column: "ConsumerBillParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerBillHistory_ConsumerPaymentInfoId",
                table: "ConsumerBillHistory",
                column: "ConsumerPaymentInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerBillParent_UserId",
                table: "ConsumerBillParent",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerPaymentInfo_ConsumerBillParentId",
                table: "ConsumerPaymentInfo",
                column: "ConsumerBillParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerPaymentInfo_PaymentMethodId",
                table: "ConsumerPaymentInfo",
                column: "PaymentMethodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumerBillHistory");

            migrationBuilder.DropTable(
                name: "ConsumerPaymentInfo");

            migrationBuilder.DropTable(
                name: "ConsumerBillParent");

            migrationBuilder.DropTable(
                name: "PaymentMethod");
        }
    }
}
