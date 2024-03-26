using System.Net.Http.Json;
using Szakdolgozat.Shared;

namespace Szakdolgozat.Client.Services.MealService
{
    public class MealService : IMealService
    {
        private HttpClient _http;

        public MealService(HttpClient http)
        {
            _http = http;
        }

        public List<Meal> Meals { get ; set ; } = new List<Meal>();
        public string Message { get; set; } = "Loading Meals...";

        public event Action MealsChanged;

        public async Task<ServiceResponse<Meal>> GetMeal(int mealId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Meal>>($"api/meal/{mealId}");
            return result;
        }

        public async Task GetMeals(string? categoryUrl = null)
        {
            var result =  categoryUrl == null ?
                await _http.GetFromJsonAsync<ServiceResponse<List<Meal>>>("api/meal"):                           //itt a kérdőjellel egy if ágat hozol létre mint c-ben 
                await _http.GetFromJsonAsync<ServiceResponse<List<Meal>>>($"api/meal/category/{categoryUrl}");
            if (result != null && result.Data != null)
                Meals = result.Data;

            MealsChanged.Invoke();
        }

        public async Task<List<string>> GetMealSearchSuggestions(string searchText)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<string>>>("api/meal/searchsuggestions/");
            return result.Data;
        }

        public async Task SearchMeals(string searchText)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Meal>>>($"api/meal/search/{searchText}");
            if (result != null && result.Data != null)
                Meals = result.Data;
            if (Meals.Count == 0) Message = "No meals found.";
                MealsChanged?.Invoke();

        }


        public async Task<Meal> UpdateProduct(Meal meal)
        {
            var result = await _http.PutAsJsonAsync($"api/meal", meal);
            var content = await result.Content.ReadFromJsonAsync<ServiceResponse<Meal>>();
            return content.Data;
        }
    }
}
