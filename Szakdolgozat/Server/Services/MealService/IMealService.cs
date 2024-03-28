using System.Transactions;

namespace Szakdolgozat.Server.Services.MealService
{
    public interface IMealService
    {
        Task<ServiceResponse<List<Meal>>> GetMealsAsync();
        Task<ServiceResponse<Meal>> GetMealAsync(int mealId);
        Task<ServiceResponse<List<Meal>>> GetMealsByCategory(string categoryUrl);

        Task<ServiceResponse<List<Meal>>> SearchMeals(string searchText);
        
        Task<ServiceResponse<List<string>>> GetMealSearchSuggestions(string searchText);

        Task<ServiceResponse<List<Meal>>> GetAdminMeals();
        Task<ServiceResponse<Meal>> CreateMeal(Meal meal);
        Task<ServiceResponse<Meal>> UpdateMeal(Meal meal);
        Task<ServiceResponse<bool>> DeleteMeal(int mealId);
    }
}
