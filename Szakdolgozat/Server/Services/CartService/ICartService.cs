namespace Szakdolgozat.Server.Services.CartService
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartMealResponseDTO>>> GetCartMeals(List<CartMeal> cartMeals);
        Task<ServiceResponse<List<CartMealResponseDTO>>> StoreCartMeals(List<CartMeal> cartMeals);
        Task<ServiceResponse<List<CartMealResponseDTO>>> GetDbCartMeals(int? userId = null);
        Task<ServiceResponse<int>> GetCartMealsCount();
        Task<ServiceResponse<bool>> AddToCart(CartMeal cartMeal);
        Task<ServiceResponse<bool>> UpdateQuantity(CartMeal cartMeal);
        Task<ServiceResponse<bool>> RemoveItemFromCart(int mealId, int mealTypeId);


    }
}
