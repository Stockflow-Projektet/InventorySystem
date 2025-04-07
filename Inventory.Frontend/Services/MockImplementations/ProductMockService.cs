using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Frontend.Services.Interfaces;
using Inventory.Frontend.Views;
using Serilog;

namespace Inventory.Frontend.Services
{
    public class ProductMockService : IProductService
    {
        private static readonly List<ProductViewModel> _products = new List<ProductViewModel>
        {
            // Example Paper product
            new ProductViewModel
            {
                ProductId = 1,
                Type = "PAP",
                Name = "A4 White Paper",
                Manufacturer = "Papermill Co.",
                Description = "High-quality A4 white paper.",
                Price = 9.99m,
                Amount = 500,
                Status = "A",
                PaperSize = "A4",
                PaperWeight = 80,
                PaperColor = "White",
                CoatingType = null
            },
            // Example Writing product
            new ProductViewModel
            {
                ProductId = 2,
                Type = "WRI",
                Name = "Gel Pen Blue",
                Manufacturer = "PenCorp",
                Description = "Smooth-writing blue gel pen.",
                Price = 1.50m,
                Amount = 100,
                Status = "A",
                InkColor = "Blue",
                InkType = "Gel",
                TipSize = 0.7m,
                PencilLeadHardness = null,
                IsErasable = false
            },
            // Example Book product
            new ProductViewModel
            {
                ProductId = 3,
                Type = "BOOK",
                Name = "The Great Gatsby",
                Manufacturer = "Vintage Books",
                Description = "Classic novel by F. Scott Fitzgerald.",
                Price = 10.99m,
                Amount = 20,
                Status = "A",
                Author = "F. Scott Fitzgerald",
                Publisher = "Charles Scribner's Sons",
                ISBN = "9780743273565",
                PublicationYear = 1925,
                NumberOfPages = 180
            },
            // Another Book
            new ProductViewModel
            {
                ProductId = 4,
                Type = "BOOK",
                Name = "C# in Depth",
                Manufacturer = "Manning",
                Description = "A deep dive into C# by Jon Skeet.",
                Price = 39.99m,
                Amount = 15,
                Status = "A",
                Author = "Jon Skeet",
                Publisher = "Manning",
                ISBN = "9781617294532",
                PublicationYear = 2019,
                NumberOfPages = 528
            }
        };

        public Task<IEnumerable<ProductViewModel>> GetProductsAsync()
        {
            Log.Verbose("Mock: Fetching all products (no filter).");
            Log.Debug("Mock: Currently we have {Count} products in memory.", _products.Count);
            return Task.FromResult(_products.AsEnumerable());
        }

        public Task<IEnumerable<ProductViewModel>> GetProductsByTypeAsync(string productType)
        {
            try
            {
                Log.Verbose("Mock: Fetching products by type: {ProductType}", productType);
                var filtered = _products.Where(p => p.Type == productType).ToList();

                if (!filtered.Any())
                {
                    Log.Warning("Mock: No products found with type {ProductType}.", productType);
                }
                else
                {
                    Log.Debug("Mock: Found {Count} products of type {ProductType}.", filtered.Count, productType);
                }

                return Task.FromResult(filtered.AsEnumerable());
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Mock: Failed to filter products by type {ProductType}.", productType);
                throw;
            }
        }

        public Task<ProductViewModel> GetProductByIdAsync(long productId)
        {
            Log.Verbose("Mock: Fetching product by ID = {ProductId}", productId);
            var product = _products.FirstOrDefault(p => p.ProductId == productId);

            if (product == null)
            {
                Log.Warning("Mock: No product found with ID {ProductId}", productId);
            }
            else
            {
                Log.Debug("Mock: Found product {Name} (ID={ProductId})", product.Name, productId);
            }

            return Task.FromResult(product);
        }

        public Task CreateProductAsync(ProductViewModel product)
        {
            if (product == null)
            {
                Log.Warning("Mock: CreateProductAsync called with null product!");
                return Task.CompletedTask;
            }

            try
            {
                Log.Information("Mock: Creating new product: {@Product}", product);
                product.ProductId = _products.Max(p => p.ProductId) + 1;
                _products.Add(product);

                Log.Information("Mock: Successfully added product with ID {ProductId}", product.ProductId);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Mock: Failed to create new product {Name}.", product.Name);
                throw;
            }
        }
    }
}
