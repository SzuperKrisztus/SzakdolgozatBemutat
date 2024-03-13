
namespace Szakdolgozat.Server.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;

        public CartService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<CartMealResponseDTO>>> GetCartMeals(List<CartMeal> cartMeals)
        {
            var result = new ServiceResponse<List<CartMealResponseDTO>>
            {
                Data = new List<CartMealResponseDTO>()
            };

            foreach (var item in cartMeals)
            {
                var meal = await _context.Meals.Where(p => p.Id == item.MealId).FirstOrDefaultAsync();

                if (meal == null)
                {
                    continue;
                }

                var mealVariant = await _context.MealVariants.Where(v => v.MealId == item.MealId && v.MealTypeId == item.MealTypeId).Include(v => v.MealType).FirstOrDefaultAsync();

                if (mealVariant == null)
                {
                    continue;
                }

                var cartMeal = new CartMealResponseDTO
                {
                    MealId = meal.Id,
                    Name = meal.Name,
                    MealType = mealVariant.MealType.Name,
                    Price = mealVariant.Price,
                    ImageUrl = meal.ImageUrl,
                    MealTypeId = mealVariant.MealTypeId

                };
                result.Data.Add(cartMeal);

            }

            return result;
        }
    }
}
