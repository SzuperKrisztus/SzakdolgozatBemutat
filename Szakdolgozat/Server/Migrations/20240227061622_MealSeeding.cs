using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Szakdolgozat.Server.Migrations
{
    public partial class MealSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Allergen", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 1, "Laktóz, glutén", "Durum spaghetti tészta, paradicsomos marha raguval és parmezánnal", "https://supervalu.ie/thumbnail/800x600/var/files/real-food/recipes/Uploaded-2020/spaghetti-bolognese-recipe.jpg", "Bolgonai Spaghetti", 1000m });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Allergen", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 2, "Glutén, Tojás, Laktóz", "Durum spaghetti tészta, tojásos pecorino romano öntettel guanchaleval", "https://www.sipandfeast.com/wp-content/uploads/2022/09/spaghetti-carbonara-recipe-snippet.jpg", "Carbonara Spaghetti", 1000m });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Allergen", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 3, "laktóz, glutén", "Nápolyi pizza paradicsomos alappal, bazsalikommal bölény mozzarellával olivaolajjal", "https://assets.biggreenegg.eu/app/uploads/2018/06/28115815/topimage-pizza-special17-800x500.jpg", "Margherita Pizza", 1000m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
