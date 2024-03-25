using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Szakdolgozat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("meals")]
        public async Task<ActionResult<ServiceResponse<List<CartMealResponseDTO>>>> GetCartMeals(List<CartMeal> cartMeals)
        {
            var result = await _cartService.GetCartMeals(cartMeals);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<CartMealResponseDTO>>>> StoreCartMeals(List<CartMeal> cartMeals)
        {
           
            var result = await _cartService.StoreCartMeals(cartMeals);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<ActionResult<ServiceResponse<bool>>> AddToCart(CartMeal cartMeal)
        {
            var result = await _cartService.AddToCart(cartMeal);
            return Ok(result);
        }

        [HttpPut("update-quantity")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateQuantity(CartMeal cartMeal)
        {
            var result = await _cartService.UpdateQuantity(cartMeal);
            return Ok(result);
        }

        [HttpDelete("{mealId}/{mealTypeId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> RemoveMealsFromCart(int mealId, int mealTypeId)
        {
            var result = await _cartService.RemoveItemFromCart(mealId, mealTypeId);
            return Ok(result);
        }

        [HttpGet("count")]
        public async Task<ActionResult<ServiceResponse<int>>> GetCartMealsCount()
        {
            return await _cartService.GetCartMealsCount();
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<CartMealResponseDTO>>>> GetDbCartMeals()
        {
            var result = await _cartService.GetDbCartMeals();
            return Ok(result);
        }
    }
}
