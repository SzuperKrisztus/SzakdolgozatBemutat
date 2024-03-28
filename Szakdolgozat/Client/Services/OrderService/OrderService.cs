
using Microsoft.AspNetCore.Components;

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

        public Task<List<OrderOverviewResponseDTO>> GetAdminOrders()
        {
            throw new NotImplementedException();
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
    }
}
