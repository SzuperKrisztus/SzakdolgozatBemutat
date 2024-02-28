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

        public event Action MealsChanged;

        public async Task<ServiceResponse<Meal>> GetMeal(int mealId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Meal>>($"api/meal/{mealId}");
            return result;
        }

        public async Task GetMeals(string? CategoryUrl = null)
        {
            var result =  CategoryUrl == null ?
                await _http.GetFromJsonAsync<ServiceResponse<List<Meal>>>("api/meal"):                           //itt a kérdőjellel egy if ágat hozol létre mint c-ben 
                await _http.GetFromJsonAsync<ServiceResponse<List<Meal>>>($"api/meal/category/{CategoryUrl}");
            if (result != null && result.Data != null)
                Meals = result.Data;

            MealsChanged.Invoke();
        }

        
    }
}
