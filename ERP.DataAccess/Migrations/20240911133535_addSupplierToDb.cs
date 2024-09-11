using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addSupplierToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TimeSet = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SupplierTaxId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierTel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierFax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SupplierContactPerson = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SupplierContactTel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierContactCellPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeSet = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
