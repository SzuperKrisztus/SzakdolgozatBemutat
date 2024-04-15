
using Microsoft.EntityFrameworkCore;

namespace Szakdolgozat.Server.Services.MealVariantService
{
    public class MealVariantService : IMealVariantService
    {
        private DataContext _context;



        public MealVariantService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;

        }

        public async Task<ServiceResponse<List<MealVariant>>> GetMealVariantsAsync()
        {
            var response = new ServiceResponse<List<MealVariant>>()
            {
                Data = await _context.MealVariants.Where(p => p.Visible && !p.Deleted)
               .Include(p => p.Meal)
               .Include(p => p.MealType)
               .ToListAsync()
            };

            return response;
        }

        public async Task<int> GetMealItemCount(int mealId)
        {
            var items = await _context.Set<OrderItem>()
            .Where(o => o.MealId == mealId)
            .ToListAsync();

            // Számoljuk össze a rekordok számát, és szorozzuk meg a Quantity értékükkel
            var totalQuantity = items.Sum(o => o.Quantity);

            return totalQuantity;
        }
    }
}
