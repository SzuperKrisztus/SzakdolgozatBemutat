namespace Szakdolgozat.Client.Services.MealTypeService
{
    public interface IMealTypeService
    {
        event Action OnChange;
        public List<MealType> MealTypes { get; set; }
        Task GetMealTypes();
        Task AddMealType(MealType mealType);
        Task UpdateMealType(MealType mealType);
        MealType CreateNewMealType();
    }
}
