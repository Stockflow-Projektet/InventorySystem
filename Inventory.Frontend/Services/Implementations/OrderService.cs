using Inventory.Frontend.Services.Interfaces;
using Inventory.Frontend.Views;

namespace Inventory.Frontend.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;

    public OrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<OrderViewModel>> GetOrdersAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<OrderViewModel>>("api/orders");
    }

    public async Task<OrderViewModel> GetOrderByIdAsync(long orderId)
    {
        return await _httpClient.GetFromJsonAsync<OrderViewModel>($"api/orders/{orderId}");
    }

    public async Task CreateOrderAsync(OrderViewModel order)
    {
        var response = await _httpClient.PostAsJsonAsync("api/orders", order);
        response.EnsureSuccessStatusCode();
    }
}