using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Szakdolgozat.Server.Migrations
{
    public partial class CartMeals_into_the_Db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartMeals",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MealId = table.Column<int>(type: "int", nullable: false),
                    MealTypeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartMeals", x => new { x.UserId, x.MealId, x.MealTypeId });
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 3, 24, 17, 15, 47, 483, DateTimeKind.Local).AddTicks(9319));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartMeals");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 3, 17, 19, 24, 23, 262, DateTimeKind.Local).AddTicks(5922));
        }
    }
}
