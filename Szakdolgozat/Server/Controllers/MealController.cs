using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Exchange.WebServices.Data;
using Szakdolgozat.Server.Data;

namespace Szakdolgozat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IMealService _mealService;

        public MealController(IMealService mealService)
        {
            _mealService = mealService;
        }



        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Meal>>>> GetMeals()
        {
            var result = await _mealService.GetMealsAsync();
            return Ok(result);
        }

        [HttpGet("{mealId}")]
        public async Task<ActionResult<ServiceResponse<List<Meal>>>> GetMeal(int mealId)
        {
            var result = await _mealService.GetMealAsync(mealId);
            return Ok(result);
        }

        [HttpGet("category/{categoryUrl}")]

        public async Task<ActionResult<ServiceResponse<List<Meal>>>> GetMealByCategory(string categoryUrl)
        {
            var result = await _mealService.GetMealsByCategory(categoryUrl);
            return Ok(result);
        }


        [HttpGet("search/{searchText}")]

        public async Task<ActionResult<ServiceResponse<List<Meal>>>> SearchMeals(string searchText)
        {
            var result = await _mealService.SearchMeals(searchText);
            return Ok(result);
        }

        [HttpGet("searchsuggestions/{searchText}")]

        public async Task<ActionResult<ServiceResponse<List<Meal>>>> GetMealsSearchSuggestions(string searchText)
        {
            var result = await _mealService.GetMealSearchSuggestions(searchText);
            return Ok(result);
        }

    }
}
