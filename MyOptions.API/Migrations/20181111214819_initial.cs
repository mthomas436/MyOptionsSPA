using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyOptions.API.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OptionTypes",
                columns: table => new
                {
                    OptionTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionTypes", x => x.OptionTypeId);
                });

            migrationBuilder.CreateTable(
                name: "TradeTypes",
                columns: table => new
                {
                    TradeTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeTypes", x => x.TradeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Userid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    PhotoUrl = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastActive = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Userid);
                });

            migrationBuilder.CreateTable(
                name: "Trades",
                columns: table => new
                {
                    Tradeid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    StockSymbol = table.Column<string>(nullable: false),
                    DateEntered = table.Column<DateTime>(nullable: false),
                    DateClosed = table.Column<DateTime>(nullable: true),
                    Userid = table.Column<int>(nullable: false),
                    TradeTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trades", x => x.Tradeid);
                    table.ForeignKey(
                        name: "FK_Trades_TradeTypes_TradeTypeId",
                        column: x => x.TradeTypeId,
                        principalTable: "TradeTypes",
                        principalColumn: "TradeTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trades_Users_Userid",
                        column: x => x.Userid,
                        principalTable: "Users",
                        principalColumn: "Userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Optionid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExpirationDate = table.Column<DateTime>(type: "Date", nullable: false),
                    StrikePrice = table.Column<double>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    DateEntered = table.Column<DateTime>(type: "Date", nullable: false),
                    EntryPrice = table.Column<double>(nullable: false),
                    StockPriceatPurchace = table.Column<double>(nullable: false),
                    ExitPrice = table.Column<double>(nullable: true),
                    Commission = table.Column<double>(nullable: true),
                    DateClosed = table.Column<DateTime>(type: "Date", nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    Tradeid = table.Column<int>(nullable: false),
                    OptionTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Optionid);
                    table.ForeignKey(
                        name: "FK_Options_OptionTypes_OptionTypeId",
                        column: x => x.OptionTypeId,
                        principalTable: "OptionTypes",
                        principalColumn: "OptionTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Options_Trades_Tradeid",
                        column: x => x.Tradeid,
                        principalTable: "Trades",
                        principalColumn: "Tradeid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Options_OptionTypeId",
                table: "Options",
                column: "OptionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_Tradeid",
                table: "Options",
                column: "Tradeid");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_TradeTypeId",
                table: "Trades",
                column: "TradeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_Userid",
                table: "Trades",
                column: "Userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "OptionTypes");

            migrationBuilder.DropTable(
                name: "Trades");

            migrationBuilder.DropTable(
                name: "TradeTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
