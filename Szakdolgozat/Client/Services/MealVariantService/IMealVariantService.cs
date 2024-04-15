namespace Szakdolgozat.Client.Services.MealVariantService
{
    public interface IMealVariantService
    {
        List<MealVariant> MealVariants { get; set; }

        Task GetMealVariants();

        Task<int> GetMealItemCount(int mealId);
    }
}
