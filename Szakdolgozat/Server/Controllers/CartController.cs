using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
