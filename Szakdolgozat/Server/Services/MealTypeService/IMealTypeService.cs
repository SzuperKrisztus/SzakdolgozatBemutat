namespace Szakdolgozat.Server.Services.MealTypeService
{
    public interface IMealTypeService
    {
        Task<ServiceResponse<List<MealType>>> GetMealTypes();
        Task<ServiceResponse<List<MealType>>> AddMealType(MealType mealType);
        Task<ServiceResponse<List<MealType>>> UpdateMealType(MealType mealType);


    }
}
