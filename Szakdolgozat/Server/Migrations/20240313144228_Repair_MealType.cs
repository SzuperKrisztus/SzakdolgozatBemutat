using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Szakdolgozat.Server.Migrations
{
    public partial class Repair_MealType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "MealVariants",
                columns: new[] { "MealId", "MealTypeId", "OriginalPrice", "Price" },
                values: new object[] { 1, 8, 2500m, 2500m });

            migrationBuilder.InsertData(
                table: "MealVariants",
                columns: new[] { "MealId", "MealTypeId", "OriginalPrice", "Price" },
                values: new object[] { 2, 9, 2200m, 2200m });

            migrationBuilder.InsertData(
                table: "MealVariants",
                columns: new[] { "MealId", "MealTypeId", "OriginalPrice", "Price" },
                values: new object[] { 3, 10, 2300m, 2300m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MealVariants",
                keyColumns: new[] { "MealId", "MealTypeId" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "MealVariants",
                keyColumns: new[] { "MealId", "MealTypeId" },
                keyValues: new object[] { 2, 9 });

            migrationBuilder.DeleteData(
                table: "MealVariants",
                keyColumns: new[] { "MealId", "MealTypeId" },
                keyValues: new object[] { 3, 10 });

            migrationBuilder.InsertData(
                table: "MealVariants",
                columns: new[] { "MealId", "MealTypeId", "OriginalPrice", "Price" },
                values: new object[] { 1, 7, 2500m, 2500m });

            migrationBuilder.InsertData(
                table: "MealVariants",
                columns: new[] { "MealId", "MealTypeId", "OriginalPrice", "Price" },
                values: new object[] { 2, 7, 2200m, 2200m });

            migrationBuilder.InsertData(
                table: "MealVariants",
                columns: new[] { "MealId", "MealTypeId", "OriginalPrice", "Price" },
                values: new object[] { 3, 7, 2300m, 2300m });
        }
    }
}
