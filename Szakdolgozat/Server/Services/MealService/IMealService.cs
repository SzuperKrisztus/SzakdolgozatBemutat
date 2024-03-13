using System.Transactions;

namespace Szakdolgozat.Server.Services.MealService
{
    public interface IMealService
    {
        Task<ServiceResponse<List<Meal>>> GetMealsAsync();
        Task<ServiceResponse<Meal>> GetMealAsync(int mealId);
        Task<ServiceResponse<List<Meal>>> GetMealsByCategory(string categoryUrl);

        Task<ServiceResponse<List<Meal>>> SearchMeals(string searchText);
       
    }
}
