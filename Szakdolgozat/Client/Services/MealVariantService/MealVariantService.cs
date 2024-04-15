namespace Szakdolgozat.Client.Services.MealVariantService
{
    public class MealVariantService : IMealVariantService
    {
        public List<MealVariant> MealVariants { get; set; } = new List<MealVariant>();

        private HttpClient _http;

        public MealVariantService(HttpClient http)
        {
            _http = http;
        }
        public async Task GetMealVariants()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<MealVariant>>>("api/mealvariant");                        //itt a kérdőjellel egy if ágat hozol létre mint c-ben 

            if (result != null && result.Data != null)
                MealVariants = result.Data;
        }

        public async Task<int> GetMealItemCount(int mealId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<int>>($"api/mealvariant/{mealId}");
            return result.Data;
        }
    }
}
