namespace Szakdolgozat.Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<bool>> PlaceOrder();
        Task<ServiceResponse<List<OrderOverviewResponseDTO>>> GetOrders();

        Task<ServiceResponse<OrderDetailsResponseDTO>> GetOrderDetails(int orderId);



        Task<ServiceResponse<List<OrderOverviewResponseDTO>>> GetAdminOrders();

        Task<ServiceResponse<bool>> UpdateStatus(int orderId, string newStatus);

        Task<ServiceResponse<bool>> DeleteOrder(int orderId);
    }
}
