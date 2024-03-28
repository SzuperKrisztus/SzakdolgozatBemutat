using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Szakdolgozat.Server.Migrations
{
    public partial class ImageClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "MealVariants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "MealVariants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MealId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "MealVariants",
                keyColumns: new[] { "MealId", "MealTypeId" },
                keyValues: new object[] { 1, 8 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "MealVariants",
                keyColumns: new[] { "MealId", "MealTypeId" },
                keyValues: new object[] { 2, 9 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "MealVariants",
                keyColumns: new[] { "MealId", "MealTypeId" },
                keyValues: new object[] { 3, 10 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "MealVariants",
                keyColumns: new[] { "MealId", "MealTypeId" },
                keyValues: new object[] { 4, 3 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "MealVariants",
                keyColumns: new[] { "MealId", "MealTypeId" },
                keyValues: new object[] { 4, 4 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "MealVariants",
                keyColumns: new[] { "MealId", "MealTypeId" },
                keyValues: new object[] { 5, 1 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "MealVariants",
                keyColumns: new[] { "MealId", "MealTypeId" },
                keyValues: new object[] { 5, 2 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "MealVariants",
                keyColumns: new[] { "MealId", "MealTypeId" },
                keyValues: new object[] { 6, 5 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "MealVariants",
                keyColumns: new[] { "MealId", "MealTypeId" },
                keyValues: new object[] { 6, 6 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "MealVariants",
                keyColumns: new[] { "MealId", "MealTypeId" },
                keyValues: new object[] { 6, 7 },
                column: "Visible",
                value: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 3, 28, 16, 38, 27, 678, DateTimeKind.Local).AddTicks(4404));

            migrationBuilder.CreateIndex(
                name: "IX_Images_MealId",
                table: "Images",
                column: "MealId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "MealVariants");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "MealVariants");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 3, 28, 15, 50, 44, 429, DateTimeKind.Local).AddTicks(3837));
        }
    }
}
