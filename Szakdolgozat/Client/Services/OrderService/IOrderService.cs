using Microsoft.AspNetCore.Components;

namespace Szakdolgozat.Client.Services.OrderService
{
    public interface IOrderService
    {
        event Action OnChange;
        Task PlaceOrder();
        List<OrderOverviewResponseDTO> AdminOrders { get; set; }
        string Message { get; set; }
        Task<List<OrderOverviewResponseDTO>> GetOrders();

        Task<OrderDetailsResponseDTO> GetOrderDetails(int orderId);

        Task<ServiceResponse<OrderOverviewResponseDTO>> GetAdminOrderDetails(int orderId);
        Task<List<OrderOverviewResponseDTO>> GetAdminOrders();
        Task<ServiceResponse<bool>> UpdateStatus(int orderId, string newStatus);

        Task<ServiceResponse<bool>> DeleteOrder(int orderId);



    }
}
