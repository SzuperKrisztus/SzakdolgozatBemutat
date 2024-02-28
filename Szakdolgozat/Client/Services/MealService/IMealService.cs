
namespace Szakdolgozat.Client.Services.MealService
{
    public interface IMealService
    {
        event Action MealsChanged;
        List<Meal> Meals { get; set; }
        Task GetMeals(string? CategoryUrl = null);

        Task<ServiceResponse<Meal>> GetMeal(int mealId);
    }
}
