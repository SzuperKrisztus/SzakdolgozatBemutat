
namespace Szakdolgozat.Client.Services.MealService
{
    public interface IMealService
    {
        event Action MealsChanged;
        List<Meal> Meals { get; set; }

        string Message { get; set; }
        Task GetMeals(string? categoryUrl = null);

        Task<ServiceResponse<Meal>> GetMeal(int mealId);

        Task SearchMeals(string searchText);

        Task<List<string>> GetMealSearchSuggestions(string searchText);


    }
}
