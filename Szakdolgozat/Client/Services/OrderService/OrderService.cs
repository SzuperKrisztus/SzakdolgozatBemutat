
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text;
using Szakdolgozat.Shared;

namespace Szakdolgozat.Client.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly NavigationManager _navigationManager;
        private readonly IAuthService _authService;

        public OrderService(HttpClient http, AuthenticationStateProvider authStateProvider, NavigationManager navigationManager, IAuthService authService)
        {
            _http = http;
            _authStateProvider = authStateProvider;
            _navigationManager = navigationManager;
            _authService = authService;
        }

        public event Action OnChange;

        public List<OrderOverviewResponseDTO> AdminOrders { get; set; }
        public string Message { get; set; } = "Rendelések betöltése...";
       

       public async Task<List<OrderOverviewResponseDTO>> GetAdminOrders()
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<OrderOverviewResponseDTO>>>("api/order/admin");
           
            return  result.Data;
        }

        public async Task<OrderDetailsResponseDTO> GetOrderDetails(int orderId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<OrderDetailsResponseDTO>>($"api/order/{orderId}");
            return result.Data;
        }
      

      

        public async Task<List<OrderOverviewResponseDTO>> GetOrders()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<OrderOverviewResponseDTO>>>("api/order");
            return result.Data;
        }

        public async Task PlaceOrder()
        {
            if (await IsUserAuthenticated() )
            {
                await _http.PostAsync("api/order", null);
            }
            else
            {
                _navigationManager.NavigateTo("login");
            }
        }
        private async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }

        public async Task<ServiceResponse<bool>> UpdateStatus(int orderId, string newStatus)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                var request = new UpdateStatusRequest
                {
                    newStatus = newStatus,
                    orderId = orderId
                };

                HttpResponseMessage httpResponse = await _http.PutAsJsonAsync("api/order/update-status", request);

                if (httpResponse.IsSuccessStatusCode)
                {
                    response = await httpResponse.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
                }
                else
                {
                    response.Success = false;
                    response.Message = $"Server returned error: {httpResponse.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error updating order status: {ex.Message}";
            }

            return response;
        }

        private class UpdateStatusRequest
        {
            public string newStatus { get; set; }
            public int orderId { get; set; }
        }



        public async Task<ServiceResponse<bool>> DeleteOrder(int orderId)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                var result = await _http.DeleteAsync($"api/order/{orderId}");

                if (result.IsSuccessStatusCode)
                {
                    response.Data = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error deleting order.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error deleting order: {ex.Message}";
            }

            return response;
        }

        public Task<ServiceResponse<OrderOverviewResponseDTO>> GetAdminOrderDetails(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
