using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fixPurchaseDetailModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseDetail",
                columns: table => new
                {
                    PurchaseDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Timeset = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDetail", x => x.PurchaseDetailId);
                    table.ForeignKey(
                        name: "FK_PurchaseDetail_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseDetail_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "PurchaseOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetail_ProductId",
                table: "PurchaseDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetail_PurchaseOrderId",
                table: "PurchaseDetail",
                column: "PurchaseOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseDetail");
        }
    }
}
