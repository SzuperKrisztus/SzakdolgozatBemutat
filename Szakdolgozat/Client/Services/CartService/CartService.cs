
using Blazored.LocalStorage;
using System.Net.Http.Json;
using Szakdolgozat.Shared;

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

            var sameItem = cart.Find(x => x.MealId == cartMeal.MealId && x.MealTypeId == cartMeal.MealTypeId);
            if (sameItem == null)
            {
                cart.Add(cartMeal);
            }
            else
            {
                sameItem.Quantity += cartMeal.Quantity;
            }

            await _localStorage.SetItemAsync("cart", cart);
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

        public async Task RemoveMealFromCart(int mealId, int mealTypeId)
        {
            var cart = await _localStorage.GetItemAsync<List<CartMeal>>("cart");
            if (cart == null)
            {
                return;
            }

            var cartItem = cart.Find(x => x.MealId == mealId && x.MealTypeId == mealTypeId);
            if (cartItem != null)
            { 
                cart.Remove(cartItem);
                await _localStorage.SetItemAsync("cart", cart);
                OnChange.Invoke();
            }
        }

        public async Task UpdateQuantity(CartMealResponseDTO meal)
        {
            var cart = await _localStorage.GetItemAsync<List<CartMeal>>("cart");
            if (cart == null)
            {
                return;
            }

            var cartItem = cart.Find(x => x.MealId == meal.MealId && x.MealTypeId == meal.MealTypeId);
            if (cartItem != null)
            {
                cartItem.Quantity = meal.Quantity;
                await _localStorage.SetItemAsync("cart", cart);
                
                
            }
        }
    }
}
