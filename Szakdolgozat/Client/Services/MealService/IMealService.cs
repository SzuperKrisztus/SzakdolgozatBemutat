
namespace Szakdolgozat.Client.Services.MealService
{
    public interface IMealService
    {
        event Action MealsChanged;
        List<Meal> Meals { get; set; }
        List<Meal> AdminMeals { get; set; }
       
        string Message { get; set; }
        Task GetMeals(string? categoryUrl = null);

        Task<ServiceResponse<Meal>> GetMeal(int mealId);

        Task SearchMeals(string searchText);

        Task<List<string>> GetMealSearchSuggestions(string searchText);

        Task GetAdminMeals();
        Task<Meal> CreateMeal(Meal meal);
        Task<Meal> UpdateMeal(Meal meal);
        Task DeleteMeal(Meal meal);
    }

}

