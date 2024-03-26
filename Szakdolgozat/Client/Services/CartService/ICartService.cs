namespace Szakdolgozat.Client.Services.CartService
{
    public interface ICartService
    {
        event Action OnChange;
        Task AddToCart (CartMeal cartMeal);
    
        Task<List<CartMealResponseDTO>> GetCartMeals();
        Task RemoveMealFromCart(int mealId, int mealTypeId);
        Task UpdateQuantity(CartMealResponseDTO meal);
        Task StoreCartMeals(bool emptyLocalCart);
      /*  Task GetCartMealsCount();*/

    }
}
