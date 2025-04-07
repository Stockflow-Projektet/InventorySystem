using Inventory.Frontend.Services.Interfaces;
using Inventory.Frontend.Views;

namespace Inventory.Frontend.Services.Implementations;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ProductViewModel>> GetProductsAsync()
    {
        // Example endpoint: "https://api.yoursite.com/products"
        return await _httpClient.GetFromJsonAsync<IEnumerable<ProductViewModel>>("api/products");
    }

    public async Task<ProductViewModel> GetProductByIdAsync(long productId)
    {
        return await _httpClient.GetFromJsonAsync<ProductViewModel>($"api/products/{productId}");
    }

    public async Task CreateProductAsync(ProductViewModel product)
    {
        var response = await _httpClient.PostAsJsonAsync("api/products", product);
        response.EnsureSuccessStatusCode();
    }
}