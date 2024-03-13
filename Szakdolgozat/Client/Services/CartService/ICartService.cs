namespace Szakdolgozat.Client.Services.CartService
{
    public interface ICartService
    {
        event Action OnChange;
        Task AddToCart (CartMeal cartMeal);
        Task<List<CartMeal>> GetCartItems ();

        Task<List<CartMealResponseDTO>> GetCartMeals();
    }
}
