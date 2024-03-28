
namespace Szakdolgozat.Server.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly ICartService _cartService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthService _authService;

        public OrderService(DataContext context, ICartService cartService,IHttpContextAccessor httpContextAccessor, IAuthService authService )
        {
            _context = context;
            _cartService = cartService;
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
        }

        public async Task<ServiceResponse<OrderDetailsResponseDTO>> GetOrderDetails(int orderId)
        {
            var response = new ServiceResponse<OrderDetailsResponseDTO>();
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Meal)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MealType)
                .Where(o => o.UserId == _authService.GetUserId() && o.Id == orderId)
                .OrderByDescending(o => o.OrderDate)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                response.Success = false;
                response.Message = "Order not found.";
                return response;
            }

            var orderDetails = new OrderDetailsResponseDTO
            {
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Meals = new List<OrderDetailsMealResponseDTO>()
            };

            order.OrderItems.ForEach(item =>
           orderDetails.Meals.Add(new OrderDetailsMealResponseDTO
           {
               MealId = item.MealId,
               ImageUrl = item.Meal.ImageUrl,
               MealType = item.MealType.Name,
               Quantity = item.Quantity,
               Name = item.Meal.Name,
               TotalPrice = item.Price
           }));

            response.Data = orderDetails;

            return response;
        }

        public async Task<ServiceResponse<List<OrderOverviewResponseDTO>>> GetOrders()
        {
            var response = new ServiceResponse<List<OrderOverviewResponseDTO>>();
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Meal)
                .Where(o => o.UserId == _authService.GetUserId())
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            var orderResponse = new List<OrderOverviewResponseDTO>();
            orders.ForEach(o => orderResponse.Add(new OrderOverviewResponseDTO
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                Meal = o.OrderItems.Count > 1 ? $"{o.OrderItems.First().Meal.Name} and "  +
                $"{o.OrderItems.Count - 1} more..." :
                o.OrderItems.First().Meal.Name,
                MealImageUrl = o.OrderItems.First().Meal.ImageUrl
            }));

            response.Data = orderResponse;
            return response;
        }

       

        public async Task<ServiceResponse<bool>> PlaceOrder()
        {
            var meals = (await _cartService.GetDbCartMeals()).Data;
            decimal totalPrice = 0;
            meals.ForEach(meal => totalPrice += meal.Price * meal.Quantity);

            var orderItems = new List<OrderItem>();
            meals.ForEach(meal => orderItems.Add(new OrderItem
            {
                MealId = meal.MealId,
                MealTypeId = meal.MealTypeId,
                Quantity = meal.Quantity,
                Price = meal.Price * meal.Quantity
            }));

            var order = new Order
            {
                UserId = _authService.GetUserId(),
                OrderDate = DateTime.Now,
                TotalPrice = totalPrice,
                OrderItems = orderItems,
                Status = "Ordered"
            };

            _context.Orders.Add(order);

            _context.CartMeals.RemoveRange(_context.CartMeals
                .Where(c => c.UserId == _authService.GetUserId()));

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }
    }
}
