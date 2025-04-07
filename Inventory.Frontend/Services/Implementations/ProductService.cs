using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Inventory.Frontend.Services.Interfaces;
using Inventory.Frontend.Views;
using Serilog;  // For logging

namespace Inventory.Frontend.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsAsync()
        {
            Log.Verbose("Fetching all products (no filter).");
            try
            {
                var result = await _httpClient.GetFromJsonAsync<IEnumerable<ProductViewModel>>("api/products");
                Log.Debug("Received product list from API.");
                return result ?? new List<ProductViewModel>();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to fetch products from API.");
                throw;
            }
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsByTypeAsync(string productType)
        {
            Log.Verbose("Fetching products by type: {ProductType}", productType);
            try
            {
                // e.g. GET /api/products?type=PAP
                var endpoint = $"api/products?type={productType}";
                var result = await _httpClient.GetFromJsonAsync<IEnumerable<ProductViewModel>>(endpoint);
                Log.Debug("Received product list filtered by {ProductType}.", productType);
                return result ?? new List<ProductViewModel>();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching products of type {ProductType}.", productType);
                throw;
            }
        }

        public async Task<ProductViewModel> GetProductByIdAsync(long productId)
        {
            Log.Verbose("Fetching product by id = {ProductId}", productId);
            try
            {
                var result = await _httpClient.GetFromJsonAsync<ProductViewModel>($"api/products/{productId}");
                if (result == null)
                {
                    Log.Warning("No product found with id {ProductId}", productId);
                }
                return result;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to fetch product with id {ProductId}.", productId);
                throw;
            }
        }

        public async Task CreateProductAsync(ProductViewModel product)
        {
            Log.Information("Creating new product: {@Product}", product);
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/products", product);
                if (!response.IsSuccessStatusCode)
                {
                    Log.Warning("CreateProductAsync failed. Status code: {StatusCode}", response.StatusCode);
                }
                response.EnsureSuccessStatusCode();
                Log.Information("Successfully created product with name: {Name}", product.Name);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to create product: {Name}", product.Name);
                throw;
            }
        }
    }
}
