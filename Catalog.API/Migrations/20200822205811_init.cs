using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalog.API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatalogBrands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    PictureFileName = table.Column<string>(nullable: true),
                    PictureUri = table.Column<string>(nullable: true),
                    CatalogTypeId = table.Column<int>(nullable: false),
                    CatalogBrandId = table.Column<int>(nullable: false),
                    AvailableStock = table.Column<int>(nullable: false),
                    RestockThreshold = table.Column<int>(nullable: false),
                    MaxStockThreshold = table.Column<int>(nullable: false),
                    OnReorder = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CatalogBrands",
                columns: new[] { "Id", "Brand" },
                values: new object[] { 2, "Apple" });

            migrationBuilder.InsertData(
                table: "CatalogTypes",
                columns: new[] { "Id", "Type" },
                values: new object[] { 1, "Clothes" });

            migrationBuilder.InsertData(
                table: "CatalogTypes",
                columns: new[] { "Id", "Type" },
                values: new object[] { 2, "Electronics" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogBrands");

            migrationBuilder.DropTable(
                name: "CatalogItems");

            migrationBuilder.DropTable(
                name: "CatalogTypes");
        }
    }
}
