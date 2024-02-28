
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
                Data = await _context.Meals.ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<Meal>> GetMealAsync(int mealId)
        {
            var response = new ServiceResponse<Meal>();
            var meal = await _context.Meals.FindAsync(mealId);
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
                    .ToListAsync()

            };

            return response;
        }

       
    }
}
