using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Szakdolgozat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<OrderOverviewResponseDTO>>>> GetOrders()
        {
            var result = await _orderService.GetOrders();
            return Ok(result);
        }

        [HttpGet("admin"), Authorize(Roles = "admin")]
        public async Task<ActionResult<ServiceResponse<List<OrderOverviewResponseDTO>>>> GetAdminOrders()
        {
            var result = await _orderService.GetAdminOrders();
            return Ok(result);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<ServiceResponse<List<OrderDetailsResponseDTO>>>> GetOrderDetails(int orderId)
        {
            var result = await _orderService.GetOrderDetails(orderId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<bool>>> PlaceOrder()
        {
            var result = await _orderService.PlaceOrder();
            return Ok(result);
        }

       
        
        [HttpPut("update-status")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateOrderStatus([FromBody] UpdateOrderStatusRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _orderService.UpdateStatus(request.OrderId, request.NewStatus);
            return Ok(response);
        }
    }

    public class UpdateOrderStatusRequest
    {
        public int OrderId { get; set; }
        public string NewStatus { get; set; }
    }
        
    
}


