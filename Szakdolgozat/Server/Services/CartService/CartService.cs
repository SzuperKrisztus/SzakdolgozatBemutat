using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace Szakdolgozat.Server.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;

        public CartService(DataContext context,IAuthService authService)
        {
            _context = context;
            _authService = authService;
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
                    MealTypeId = mealVariant.MealTypeId,
                    Quantity = item.Quantity

                };
                result.Data.Add(cartMeal);

            }

            return result;
        }

        public async Task<ServiceResponse<List<CartMealResponseDTO>>> StoreCartMeals(List<CartMeal> cartMeals)
        {
           cartMeals.ForEach(cartMeal => cartMeal.UserId = _authService.GetUserId());
            _context.CartMeals.AddRange(cartMeals);
            await _context.SaveChangesAsync();

            return await GetDbCartMeals();
            // return await GetCartMeals(await _context.CartMeals.Where(c => c.UserId == _authService.GetUserId()).ToListAsync());
        }

       

        public async Task<ServiceResponse<List<CartMealResponseDTO>>> GetDbCartMeals(int? userId = null)
        {
            if (userId == null)
                userId = _authService.GetUserId();

            return await GetCartMeals(await _context.CartMeals
                .Where(ci => ci.UserId == userId).ToListAsync());
        }

        public async Task<ServiceResponse<bool>> AddToCart(CartMeal cartMeal)
        {
            cartMeal.UserId = _authService.GetUserId();

            var sameItem = await _context.CartMeals
                .FirstOrDefaultAsync(ci => ci.MealId == cartMeal.MealId &&
                ci.MealTypeId == cartMeal.MealTypeId && ci.UserId == cartMeal.UserId);
            if (sameItem == null)
            {
                _context.CartMeals.Add(cartMeal);
            }
            else
            {
                sameItem.Quantity += cartMeal.Quantity;
            }

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> UpdateQuantity(CartMeal cartMeal)
        {
            var dbCartItem = await _context.CartMeals
                 .FirstOrDefaultAsync(ci => ci.MealId == cartMeal.MealId  &&
                 ci.MealTypeId == cartMeal.MealTypeId && ci.UserId == _authService.GetUserId());
            if (dbCartItem == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Cart item does not exist."
                };
            }

            dbCartItem.Quantity = cartMeal.Quantity;
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> RemoveItemFromCart(int mealId, int mealTypeId)
        {
            var dbCartItem = await _context.CartMeals
                .FirstOrDefaultAsync(ci => ci.MealId == mealId &&
                ci.MealTypeId == mealTypeId && ci.UserId == _authService.GetUserId());
            if (dbCartItem == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Cart item does not exist."
                };
            }

            _context.CartMeals.Remove(dbCartItem);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<int>> GetCartMealsCount()
        {
            var count = (await _context.CartMeals.Where(ci => ci.UserId == _authService.GetUserId()).ToListAsync()).Count;
            return new ServiceResponse<int> { Data = count };
        }
    }
}
