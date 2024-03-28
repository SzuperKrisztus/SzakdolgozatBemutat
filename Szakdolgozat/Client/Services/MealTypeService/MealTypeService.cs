namespace Szakdolgozat.Client.Services.MealTypeService
{
    public class MealTypeService : IMealTypeService
    {
        private readonly HttpClient _http;

        public MealTypeService(HttpClient http)
        {
            _http = http;
        }

        public List<MealType> MealTypes { get; set; } = new List<MealType>();

        public event Action OnChange;

        public async Task AddMealType(MealType mealType)
        {
            var response = await _http.PostAsJsonAsync("api/mealtype", mealType);
            MealTypes = (await response.Content
                .ReadFromJsonAsync<ServiceResponse<List<MealType>>>()).Data;
            OnChange.Invoke();
        }

        public MealType CreateNewMealType()
        {
            var newMealType = new MealType { IsNew = true, Editing = true };

            MealTypes.Add(newMealType);
            OnChange.Invoke();
            return newMealType;
        }

        public async Task GetMealTypes()
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<MealType>>>("api/mealtype");
            MealTypes = result.Data;
        }

        public async Task UpdateMealType(MealType mealType)
        {
            var response = await _http.PutAsJsonAsync("api/mealtype", mealType);
            MealTypes = (await response.Content
                .ReadFromJsonAsync<ServiceResponse<List<MealType>>>()).Data;
            OnChange.Invoke();
        }
    }
}
