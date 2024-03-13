
namespace Szakdolgozat.Server.Services.MealService
{
    public class MealService : IMealService
    {
        private DataContext _context;

        public MealService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<Meal>>> GetMealsAsync()
        {
            var response = new ServiceResponse<List<Meal>>()
            {
                Data = await _context.Meals.Include(p => p.Variants).ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<Meal>> GetMealAsync(int mealId)
        {
            var response = new ServiceResponse<Meal>();
            var meal = await _context.Meals
                .Include(p => p.Variants)
                .ThenInclude(v => v.MealType)
                .FirstOrDefaultAsync(p => p.Id == mealId);
            if (meal == null)
            {
                response.Success = false;
                response.Message = "Sorry, but this meal dosen't exist.";
            }
            else
            {
                response.Data = meal;
            }
            return response;
        }

        /*public async Task<ServiceResponse<List<Meal>>> GetMealsByCategory(string categoryUrl)
        {
            var response = new ServiceResponse<List<Meal>>
            {
                Data = await _context.Meals
                    .Where(p => p.Category.Url.ToLower().Equals(categoryUrl.ToLower()))
                    .ToListAsync()

            };

            return response;
        }*/

        public async Task<ServiceResponse<List<Meal>>> GetMealsByCategory(string categoryUrl)
        {
            var response = new ServiceResponse<List<Meal>>
            {
                Data = await _context.Meals
                .Where(p => p.Category.Url.ToLower().Equals(categoryUrl.ToLower()))
                .Include(p => p.Variants)
                .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<List<Meal>>> SearchMeals(string searchText)
        {
            var response = new ServiceResponse<List<Meal>>
            {
                Data = await FindMealsBySearchText(searchText)
            };
            return response;


        }

        private async Task<List<Meal>> FindMealsBySearchText(string searchText)
        {
            return await _context.Meals
                                .Where(p => p.Name.ToLower().Contains(searchText.ToLower()) || p.Description.ToLower().Contains(searchText.ToLower()) || p.Allergen.ToLower().Contains(searchText.ToLower()))
                                .Include(p => p.Variants)
                                .ToListAsync();
        }

        public async Task<ServiceResponse<List<string>>> GetMealSearchSuggestions(string searchText)
        {
            var meals =  await FindMealsBySearchText(searchText);

            List<string> result = new List<string>();
            foreach (var meal in meals)
            {
                if (meal.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(meal.Name);
                }

                if (meal.Description != null)
                {
                    var punctuation = meal.Description.Where(char.IsPunctuation)
                        .Distinct()
                        .ToArray();
                    var words = meal.Description.Split()
                        .Select(x => x.Trim(punctuation));

                    foreach (var word in words)
                    {
                        if (word.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                            && ! result.Contains(word))
                        {
                            result.Add(word);
                        }
                    }
                }
                if (meal.Allergen != null)
                {
                    var punctuation = meal.Allergen.Where(char.IsPunctuation)
                        .Distinct()
                        .ToArray();
                    var words = meal.Allergen.Split()
                        .Select(x => x.Trim(punctuation));

                    foreach (var word in words)
                    {
                        if (word.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                            && !result.Contains(word))
                        {
                            result.Add(word);
                        }
                    }
                }
            }

            return new ServiceResponse<List<string>> { Data = result };
        }
    }
}
