
using Blazored.LocalStorage;
using System.Net.Http.Json;

namespace Szakdolgozat.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;

        public CartService(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }

        public event Action OnChange;

        public async Task AddToCart(CartMeal cartMeal)
        {
            var cart = await _localStorage.GetItemAsync<List<CartMeal>>("cart");
            if (cart == null)
            {
                cart = new List<CartMeal>();
            }
            cart.Add(cartMeal);

            await _localStorage.SetItemAsync("cart", cart);
            OnChange?.Invoke();
        }

        public async Task<List<CartMeal>> GetCartItems()
        {
            var cart = await _localStorage.GetItemAsync<List<CartMeal>>("cart");
            if (cart == null)
            {
                cart = new List<CartMeal>();
            }

            return cart;
        }

        public async Task<List<CartMealResponseDTO>> GetCartMeals()
        {
           var cartItems = await _localStorage.GetItemAsync<List<CartMeal>>("cart");
           var response = await _http.PostAsJsonAsync("api/cart/meals", cartItems);
           var cartMeals = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartMealResponseDTO>>>();
            return cartMeals.Data;


        }
    }
}
