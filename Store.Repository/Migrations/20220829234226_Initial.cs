using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Repository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "APP",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    APP_NAME = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APP", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    USER_NAME = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    EMAIL = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    HASH = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    CPF = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    STREET = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    Neighborhood = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZIP_CODE = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: false),
                    STREET_NUMBER = table.Column<decimal>(type: "NUMERIC", nullable: false),
                    COMPLEMENT = table.Column<string>(type: "VARCHAR(80)", maxLength: 80, nullable: true),
                    BIRTHDATE = table.Column<DateTime>(type: "DATE", nullable: false),
                    GENDER = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CREDIT_CARD",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREDIT_CARD_NUMBER = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    NAME_IN_CREDIT_CARD = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: false),
                    EXPIRATION_DATE = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CREDIT_CARD", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USER_CARD",
                        column: x => x.UserId,
                        principalTable: "USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ORDER",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    creditCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDER", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CREDITCARD_ORDER",
                        column: x => x.creditCardId,
                        principalTable: "CREDIT_CARD",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ORDER_USER_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ORDER_APP",
                columns: table => new
                {
                    App_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDER_APP", x => new { x.App_Id, x.OrderId });
                    table.ForeignKey(
                        name: "FK_OrderApp_AppId",
                        column: x => x.App_Id,
                        principalTable: "APP",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderApp_OrderId",
                        column: x => x.OrderId,
                        principalTable: "ORDER",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CREDIT_CARD_UserId",
                table: "CREDIT_CARD",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_BuyerId",
                table: "ORDER",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_creditCardId",
                table: "ORDER",
                column: "creditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_APP_OrderId",
                table: "ORDER_APP",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_CPF",
                table: "USER",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USER_EMAIL",
                table: "USER",
                column: "EMAIL",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ORDER_APP");

            migrationBuilder.DropTable(
                name: "APP");

            migrationBuilder.DropTable(
                name: "ORDER");

            migrationBuilder.DropTable(
                name: "CREDIT_CARD");

            migrationBuilder.DropTable(
                name: "USER");
        }
    }
}
