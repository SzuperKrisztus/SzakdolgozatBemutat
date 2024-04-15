using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text.Json;
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

        public List<Meal> Meals { get; set; } = new List<Meal>();
        public string Message { get; set; } = "Loading Meals...";
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;

        public event Action MealsChanged;
        public List<Meal> AdminMeals { get; set; }




        public async Task<Meal> CreateMeal(Meal meal)
        {
            var result = await _http.PostAsJsonAsync("api/meal", meal);
            var newMeal = (await result.Content
                .ReadFromJsonAsync<ServiceResponse<Meal>>()).Data;
            return newMeal;

            
        }

        public async Task<string> DeleteMeal(Meal meal)
        {
            var result = await _http.DeleteAsync($"api/meal/{meal.Id}");
            var deleteSuccession = result.Content;
            // Ellenőrizzük a választ
            if (deleteSuccession != null && deleteSuccession != null)
            {
                string jsonContent = await deleteSuccession.ReadAsStringAsync();
                var jsonObject = JsonSerializer.Deserialize<JsonElement>(jsonContent);

                if (jsonObject.TryGetProperty("message", out var messageProperty))
                {
                    return messageProperty.ToString();
                }
                return "Hibás válasz.";
            }
            return string.Empty;

        }

        public async Task GetAdminMeals()
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<Meal>>>("api/meal/admin");
            AdminMeals = result.Data;
            if (AdminMeals.Count == 0)
                Message = "Nincs ilyen étel.";
        }



        public async Task<ServiceResponse<Meal>> GetMeal(int mealId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Meal>>($"api/meal/{mealId}");
            return result;
        }

        public async Task GetMeals(string? categoryUrl = null)
        {
            var result = categoryUrl == null ?
                await _http.GetFromJsonAsync<ServiceResponse<List<Meal>>>("api/meal") :                           //itt a kérdőjellel egy if ágat hozol létre mint c-ben 
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
            if (Meals.Count == 0) Message = "Nincs ilyen étel.";
            MealsChanged?.Invoke();

        }


        public async Task<Meal> UpdateMeal(Meal meal)
        {
            var result = await _http.PutAsJsonAsync($"api/meal", meal);
            var content = await result.Content.ReadFromJsonAsync<ServiceResponse<Meal>>();
            return content.Data;
        }
    }
}