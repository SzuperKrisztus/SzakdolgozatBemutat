using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Szakdolgozat.Server.Migrations
{
    public partial class MealVariants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Meals");

            migrationBuilder.CreateTable(
                name: "MealTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealVariants",
                columns: table => new
                {
                    MealId = table.Column<int>(type: "int", nullable: false),
                    MealTypeId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OriginalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealVariants", x => new { x.MealId, x.MealTypeId });
                    table.ForeignKey(
                        name: "FK_MealVariants_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealVariants_MealTypes_MealTypeId",
                        column: x => x.MealTypeId,
                        principalTable: "MealTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MealTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Vörös bor" },
                    { 2, "Fehér bor" },
                    { 3, "Premium Lager" },
                    { 4, "Ale" },
                    { 5, "Epres" },
                    { 6, "Mangós" },
                    { 7, "Licsis" }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Allergen", "CategoryId", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 4, "Alkohol", 3, "0,5 Liter csapolt sör", "https://proaktivdirekt.com/adaptive/article_md/upload/images/magazine/sor.jpg", "Sör" },
                    { 5, "alkohol", 3, "A frizbiolaj pincészet külömböző típusú borai az ár dl-ben értendő", "https://joopince.hu/wp-content/uploads/2022/02/bor-fajtak.jpg", "Bor" },
                    { 6, "nincs, kivéve az aktuális gyömölcs", 3, "Házi limonádi valódi gyümölcsből", "https://receptneked.hu/wp-content/uploads/2020/08/104237976_s.jpg", "Limonádé" }
                });

            migrationBuilder.InsertData(
                table: "MealVariants",
                columns: new[] { "MealId", "MealTypeId", "OriginalPrice", "Price" },
                values: new object[,]
                {
                    { 4, 3, 800.00m, 1000.00m },
                    { 5, 1, 0.00m, 400.00m },
                    { 5, 2, 0.00m, 300.00m },
                    { 6, 5, 1000.00m, 1000.00m },
                    { 6, 6, 1000.00m, 1000.00m },
                    { 6, 7, 1000.00m, 1000.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealVariants_MealTypeId",
                table: "MealVariants",
                column: "MealTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MealVariants");

            migrationBuilder.DropTable(
                name: "MealTypes");

            migrationBuilder.DeleteData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Meals",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 1000m);

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 1000m);

            migrationBuilder.UpdateData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 1000m);
        }
    }
}
