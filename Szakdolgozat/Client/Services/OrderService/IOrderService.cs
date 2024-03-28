﻿namespace Szakdolgozat.Client.Services.OrderService
{
    public interface IOrderService
    {
        Task PlaceOrder();

        Task<List<OrderOverviewResponseDTO>> GetOrders();

        Task<OrderDetailsResponseDTO> GetOrderDetails(int orderId);

        Task<List<OrderOverviewResponseDTO>> GetAdminOrders();
    }
}
