using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Inventory.Frontend.Services.Interfaces;
using Inventory.Frontend.Views;
using Serilog;

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
            Log.Verbose("ProductService: Fetching all products (no filter).");
            try
            {
                var result = await _httpClient.GetFromJsonAsync<IEnumerable<ProductViewModel>>("api/products");
                Log.Debug("Received product list from API. Count={Count}", result?.Count() ?? 0);
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
            Log.Verbose("ProductService: Fetching products by type: {ProductType}", productType);
            try
            {
                var endpoint = $"api/products?type={productType}";
                var result = await _httpClient.GetFromJsonAsync<IEnumerable<ProductViewModel>>(endpoint);
                Log.Debug("Received product list filtered by {ProductType}, Count={Count}",
                    productType, result?.Count() ?? 0);
                return result ?? new List<ProductViewModel>();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching products of type {ProductType}.", productType);
                throw;
            }
        }

        public async Task<ProductViewModel> GetProductByIdAsync(int productId)
        {
            Log.Verbose("ProductService: Fetching product by ID={ProductId}", productId);
            try
            {
                var result = await _httpClient.GetFromJsonAsync<ProductViewModel>($"api/products/{productId}");
                if (result == null)
                {
                    Log.Warning("No product found with ID={ProductId}", productId);
                }
                else
                {
                    Log.Debug("Successfully fetched product '{Name}' (ID={ProductId})", result.Name, productId);
                }
                return result;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to fetch product with ID={ProductId}.", productId);
                throw;
            }
        }

        public async Task CreateProductAsync(ProductViewModel productVm)
        {
            Log.Verbose("CreateProductAsync started with data: {@ProductVm}", productVm);

            // (Optional) If your API expects the exact same shape, just post productVm.
            // Otherwise, you might map to a dedicated DTO. For illustration, we do:

            var productArgs = new ProductViewModel
            {
                Type = productVm.Type,
                Name = productVm.Name,
                Description = productVm.Description,
                Price = productVm.Price,
                Status = productVm.Status,
                // Book
                Author = productVm.Author,
                Publisher = productVm.Publisher,
                PublicationYear = productVm.PublicationYear,
                NumberOfPages = productVm.NumberOfPages,
                // Paper
                PaperSize = productVm.PaperSize,
                PaperWeight = productVm.PaperWeight,
                PaperColor = productVm.PaperColor,
                CoatingType = productVm.CoatingType,
                // Writing
                InkColor = productVm.InkColor,
                InkType = productVm.InkType,
                TipSize = productVm.TipSize,
                PencilLeadHardness = productVm.PencilLeadHardness,
                IsErasable = productVm.IsErasable
            };

            Log.Debug("Posting new product to /api/products. Payload: {@productArgs}", productArgs);

            var response = await _httpClient.PostAsJsonAsync("api/products", productArgs);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                Log.Warning("CreateProductAsync failed. Status: {StatusCode}, Body: {Body}",
                    response.StatusCode, errorBody);

                // We'll throw or you can do something else here:
                response.EnsureSuccessStatusCode(); // This will throw an exception
            }
            else
            {
                Log.Information("CreateProductAsync success for product: {Name}", productVm.Name);
            }
        }
    }
}
