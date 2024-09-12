using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addStockToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    StockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StockTel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StockPersonInChange = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StockPersonInChangeCellPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeSet = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.StockId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}
