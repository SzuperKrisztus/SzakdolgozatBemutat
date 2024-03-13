namespace Szakdolgozat.Server.Services.CartService
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartMealResponseDTO>>> GetCartMeals(List<CartMeal> cartMeals);
        

    }
}
