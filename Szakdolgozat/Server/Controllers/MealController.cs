using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Szakdolgozat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private static List<Meal> Meals = new List<Meal>
        {
               new Meal
               {
                   Id = 1,
                   Name= "Bolgonai Spaghetti",
                   Description = "Durum spaghetti tészta, paradicsomos marha raguval és parmezánnal",
                   Allergen = "Laktóz, glutén",
                   Price = 1000,
                   ImageUrl = "https://supervalu.ie/thumbnail/800x600/var/files/real-food/recipes/Uploaded-2020/spaghetti-bolognese-recipe.jpg"

               },

               new Meal
               {
                   Id = 2,
                   Name= "Carbonara Spaghetti",
                   Description = "Durum spaghetti tészta, tojásos pecorino romano öntettel guanchaleval",
                   Allergen = "Glutén, Tojás, Laktóz",
                   Price = 1000,
                   ImageUrl = "https://www.sipandfeast.com/wp-content/uploads/2022/09/spaghetti-carbonara-recipe-snippet.jpg"

               },

               new Meal
               {
                   Id = 3,
                   Name= "Margherita Pizza",
                   Description = "Nápolyi pizza paradicsomos alappal, bazsalikommal bölény mozzarellával olivaolajjal",
                   Allergen = "laktóz, glutén",
                   Price = 1000,
                   ImageUrl = "https://assets.biggreenegg.eu/app/uploads/2018/06/28115815/topimage-pizza-special17-800x500.jpg"

               }
        };

        [HttpGet]
        public async Task<ActionResult<List<Meal>>> GetMeal()
        {
            return Ok(Meals);
        }
    }
}
