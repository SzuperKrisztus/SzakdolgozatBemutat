using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Szakdolgozat.Server.Migrations
{
    public partial class User_Megint_merr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealVariant_Meals_MealId",
                table: "MealVariant");

            migrationBuilder.DropForeignKey(
                name: "FK_MealVariant_MealTypes_MealTypeId",
                table: "MealVariant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealVariant",
                table: "MealVariant");

            migrationBuilder.RenameTable(
                name: "MealVariant",
                newName: "MealVariants");

            migrationBuilder.RenameIndex(
                name: "IX_MealVariant_MealTypeId",
                table: "MealVariants",
                newName: "IX_MealVariants_MealTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealVariants",
                table: "MealVariants",
                columns: new[] { "MealId", "MealTypeId" });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Created", "Email", "PasswordHash", "PasswordSalt", "Role" },
                values: new object[] { 1, new DateTime(2024, 3, 17, 19, 24, 23, 262, DateTimeKind.Local).AddTicks(5922), "user@user.com", new byte[] { 1, 2, 3, 4, 5 }, new byte[] { 1, 2, 3, 4, 5 }, "user" });

            migrationBuilder.AddForeignKey(
                name: "FK_MealVariants_Meals_MealId",
                table: "MealVariants",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealVariants_MealTypes_MealTypeId",
                table: "MealVariants",
                column: "MealTypeId",
                principalTable: "MealTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealVariants_Meals_MealId",
                table: "MealVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_MealVariants_MealTypes_MealTypeId",
                table: "MealVariants");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealVariants",
                table: "MealVariants");

            migrationBuilder.RenameTable(
                name: "MealVariants",
                newName: "MealVariant");

            migrationBuilder.RenameIndex(
                name: "IX_MealVariants_MealTypeId",
                table: "MealVariant",
                newName: "IX_MealVariant_MealTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealVariant",
                table: "MealVariant",
                columns: new[] { "MealId", "MealTypeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MealVariant_Meals_MealId",
                table: "MealVariant",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealVariant_MealTypes_MealTypeId",
                table: "MealVariant",
                column: "MealTypeId",
                principalTable: "MealTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
