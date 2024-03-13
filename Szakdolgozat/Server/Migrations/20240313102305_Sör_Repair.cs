using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Szakdolgozat.Server.Migrations
{
    public partial class Sör_Repair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MealVariants",
                keyColumns: new[] { "MealId", "MealTypeId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.InsertData(
                table: "MealVariants",
                columns: new[] { "MealId", "MealTypeId", "OriginalPrice", "Price" },
                values: new object[] { 4, 4, 800.00m, 1200.00m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MealVariants",
                keyColumns: new[] { "MealId", "MealTypeId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.InsertData(
                table: "MealVariants",
                columns: new[] { "MealId", "MealTypeId", "OriginalPrice", "Price" },
                values: new object[] { 4, 2, 800.00m, 1200.00m });
        }
    }
}
