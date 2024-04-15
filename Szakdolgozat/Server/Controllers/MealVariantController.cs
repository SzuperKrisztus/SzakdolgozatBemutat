using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Szakdolgozat.Server.Services.MealVariantService;

namespace Szakdolgozat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealVariantController : ControllerBase
    {
        private readonly IMealVariantService _mealVariantService;
        private readonly DataContext _context;

        public MealVariantController(IMealVariantService mealVariantService, DataContext context )
        {
            _mealVariantService = mealVariantService;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Meal>>>> GetMealVariants()
        {
            var result = await _mealVariantService.GetMealVariantsAsync();
            return Ok(result);
        }

        [HttpGet("{mealId}")]
        public async Task<ActionResult<ServiceResponse<int>>> GetMealItemCount(int mealId)
        {
            var items = await _context.Set<OrderItem>()
         .Where(o => o.MealId == mealId)
         .ToListAsync();

            var totalQuantity = items.Sum(o => o.Quantity);

            // Visszaadás JSON formátumban
            return Ok(new { Data = totalQuantity });
        }



    }
}
