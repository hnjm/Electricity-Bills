using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Initail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Line",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LineName = table.Column<string>(maxLength: 50, nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(8, 2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Line", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: true),
                    PassWord = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerName = table.Column<string>(maxLength: 50, nullable: true),
                    CustomerMobile = table.Column<string>(nullable: true),
                    LineId = table.Column<int>(nullable: true),
                    MinimumAmount = table.Column<int>(nullable: true),
                    CounterNumber = table.Column<int>(nullable: true),
                    LastBalance = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    CustomerStatue = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_Line",
                        column: x => x.LineId,
                        principalTable: "Line",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CounterReads",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(nullable: true),
                    DateOfRead = table.Column<DateTime>(type: "date", nullable: true),
                    TheRead = table.Column<long>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CounterReads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CounterReads_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerBills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(nullable: true),
                    LineId = table.Column<int>(nullable: true),
                    DateOfLastRead = table.Column<DateTime>(type: "date", nullable: true),
                    PeriodOfBill = table.Column<string>(maxLength: 50, nullable: true),
                    MinimumAmount = table.Column<int>(nullable: true),
                    LastRead = table.Column<long>(nullable: true),
                    CurrentRead = table.Column<long>(nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    MaintenanceFees = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    ServicesFees = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    LastBalance = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    BillAmount = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerBills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerBills_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerBills_Line",
                        column: x => x.LineId,
                        principalTable: "Line",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(nullable: true),
                    DateOfPay = table.Column<DateTime>(type: "date", nullable: true),
                    Payment = table.Column<decimal>(type: "decimal(8, 2)", nullable: true),
                    Sanad = table.Column<int>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CounterReads_CustomerId",
                table: "CounterReads",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_LineId",
                table: "Customer",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBills_CustomerId",
                table: "CustomerBills",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBills_LineId",
                table: "CustomerBills",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_CustomerId",
                table: "Payment",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CounterReads");

            migrationBuilder.DropTable(
                name: "CustomerBills");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Line");
        }
    }
}
