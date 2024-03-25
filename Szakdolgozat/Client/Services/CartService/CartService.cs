
using Blazored.LocalStorage;
using System.Net.Http.Json;
using Szakdolgozat.Client.Pages;
using Szakdolgozat.Shared;

namespace Szakdolgozat.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;
        private readonly IAuthService _authService;

        public CartService(ILocalStorageService localStorage, HttpClient http, IAuthService authService)
        {
            _localStorage = localStorage;
            _http = http;
            _authService = authService;
            
        }

        public event Action OnChange;

        public async Task AddToCart(CartMeal cartMeal)
        {
            if (await _authService.IsUserAuthenticated())
            {
                await _http.PostAsJsonAsync("api/cart/add", cartMeal);
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartMeal>>("cart");
                if (cart == null)
                {
                    cart = new List<CartMeal>();
                }

                var sameItem = cart.Find(x => x.MealId == cartMeal.MealId &&
                    x.MealTypeId == cartMeal.MealTypeId);
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

            await GetCartMealsCount();
        }

        public async Task GetCartMealsCount()
        {
            if (await _authService.IsUserAuthenticated())
            {
                var result = await _http.GetFromJsonAsync<ServiceResponse<int>>("api/cart/count");
                var count = result.Data;

                await _localStorage.SetItemAsync<int>("cartMealsCount", count);
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartMeal>>("cart");
               await _localStorage.SetItemAsync<int>("cartMealsCount", cart != null ? cart.Count : 0);
            }

            await GetCartMealsCount();
        }

     

        public async Task<List<CartMealResponseDTO>> GetCartMeals()
        {
            if (await _authService.IsUserAuthenticated())
            {
                var response = await _http.GetFromJsonAsync<ServiceResponse<List<CartMealResponseDTO>>>("api/cart");
                return response.Data;
            }
            else
            {
                var cartItems = await _localStorage.GetItemAsync<List<CartMeal>>("cart");
                if (cartItems == null)
                    return new List<CartMealResponseDTO>();
                var response = await _http.PostAsJsonAsync("api/cart/meals", cartItems);
                var cartMeals =
                    await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartMealResponseDTO>>>();
                return cartMeals.Data;
            }
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
                await GetCartMealsCount();
            }
        }

        public async Task StoreCartMeals(bool emptyLocalCart = false)
        {
            var localCart = await _localStorage.GetItemAsync<List<CartMeal>>("cart");
            if (localCart == null)
            {
                return;
            }

            await _http.PostAsJsonAsync("api/cart", localCart);

            if (emptyLocalCart)
            {
                await _localStorage.RemoveItemAsync("cart");
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
