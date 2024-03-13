using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Szakdolgozat.Server.Migrations
{
    public partial class All_Meal_must_have_a_Variant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MealTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 8, "Bolognai Spaghetti" },
                    { 9, "Carbonara Spaghetti" },
                    { 10, "Margharita Pizza" }
                });

            migrationBuilder.InsertData(
                table: "MealVariants",
                columns: new[] { "MealId", "MealTypeId", "OriginalPrice", "Price" },
                values: new object[,]
                {
                    { 1, 7, 2500m, 2500m },
                    { 2, 7, 2200m, 2200m },
                    { 3, 7, 2300m, 2300m },
                    { 4, 2, 800.00m, 1200.00m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MealTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MealTypes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MealTypes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MealVariants",
                keyColumns: new[] { "MealId", "MealTypeId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "MealVariants",
                keyColumns: new[] { "MealId", "MealTypeId" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "MealVariants",
                keyColumns: new[] { "MealId", "MealTypeId" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "MealVariants",
                keyColumns: new[] { "MealId", "MealTypeId" },
                keyValues: new object[] { 4, 2 });
        }
    }
}
