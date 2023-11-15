using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaySpace_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlatRateTaxRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxRate = table.Column<decimal>(type: "decimal(18,3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatRateTaxRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlatValueTaxRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IncomeThreshold = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FlatTaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxRateBelowThreshold = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatValueTaxRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostalCodeTaxTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalculationType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCodeTaxTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxCalculations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnnualIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CalculatedTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CalculationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxCalculations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FromIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ToIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserIncomeDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnnualIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserIncomeDetails", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "FlatRateTaxRules",
                columns: new[] { "Id", "TaxRate" },
                values: new object[] { 1, 0.175m });

            migrationBuilder.InsertData(
                table: "FlatValueTaxRules",
                columns: new[] { "Id", "FlatTaxAmount", "IncomeThreshold", "TaxRateBelowThreshold" },
                values: new object[] { 1, 10000m, 200000m, 0.05m });

            migrationBuilder.InsertData(
                table: "PostalCodeTaxTypes",
                columns: new[] { "Id", "CalculationType", "PostalCode" },
                values: new object[,]
                {
                    { 1, 0, "7441" },
                    { 2, 1, "A100" },
                    { 3, 2, "7000" },
                    { 4, 0, "1000" }
                });

            migrationBuilder.InsertData(
                table: "TaxRates",
                columns: new[] { "Id", "FromIncome", "Rate", "ToIncome" },
                values: new object[,]
                {
                    { 1, 0m, 0.10m, 8350m },
                    { 2, 8351m, 0.15m, 33950m },
                    { 3, 33951m, 0.25m, 82250m },
                    { 4, 82251m, 0.28m, 171550m },
                    { 5, 171551m, 0.33m, 372950m },
                    { 6, 372951m, 0.35m, 100000000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlatRateTaxRules");

            migrationBuilder.DropTable(
                name: "FlatValueTaxRules");

            migrationBuilder.DropTable(
                name: "PostalCodeTaxTypes");

            migrationBuilder.DropTable(
                name: "TaxCalculations");

            migrationBuilder.DropTable(
                name: "TaxRates");

            migrationBuilder.DropTable(
                name: "UserIncomeDetails");
        }
    }
}
