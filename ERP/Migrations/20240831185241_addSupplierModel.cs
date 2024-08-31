using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    /// <inheritdoc />
    public partial class addSupplierModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierTaxIDNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierPersonInCharge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierTel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierWeb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierContactTel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierPaymentMethod = table.Column<int>(type: "int", nullable: true),
                    SupplierNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierTimeset = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
