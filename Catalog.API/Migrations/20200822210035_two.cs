using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalog.API.Migrations
{
    public partial class two : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CatalogBrands",
                columns: new[] { "Id", "Brand" },
                values: new object[] { 1, "Nokia" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CatalogBrands",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
