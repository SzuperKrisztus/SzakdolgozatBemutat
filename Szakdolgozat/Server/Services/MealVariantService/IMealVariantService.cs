namespace Szakdolgozat.Server.Services.MealVariantService
{
    public interface IMealVariantService
    {
        Task<ServiceResponse<List<MealVariant>>> GetMealVariantsAsync();
        Task<int> GetMealItemCount(int mealId);
    }

}
