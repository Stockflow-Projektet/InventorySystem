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
        // In-memory mock list of ProductViewModel
        private static readonly List<ProductViewModel> _products = new List<ProductViewModel>
        {
            // Example Paper
            new ProductViewModel
            {
                ProductId = 1,
                Type = "P",
                Name = "A4 White Paper",
                Description = "High-quality A4 white paper.",
                Price = 9.99m,
                // Paper-specific
                PaperSize = "A4",
                PaperWeight = 80,
                PaperColor = "White"
                // CoatingType = null  (omitted for brevity)
            },
            // Example Writing
            new ProductViewModel
            {
                ProductId = 2,
                Type = "W",
                Name = "Gel Pen Blue",
                Description = "Smooth-writing blue gel pen.",
                Price = 1.50m,
                // Writing-specific
                InkColor = "Blue",
                InkType = "Gel",
                TipSize = 0.7m,
                IsErasable = false
            },
            // Example Book
            new ProductViewModel
            {
                ProductId = 3,
                Type = "B",
                Name = "The Great Gatsby",
                Description = "Classic novel by F. Scott Fitzgerald.",
                Price = 10.99m,
                // Book-specific
                Author = "F. Scott Fitzgerald",
                Publisher = "Charles Scribner's Sons",
                PublicationYear = 1925,
                NumberOfPages = 180
            },
            // Another Book
            new ProductViewModel
            {
                ProductId = 4,
                Type = "B",
                Name = "C# in Depth",
                Description = "A deep dive into C# by Jon Skeet.",
                Price = 39.99m,
                // Book-specific
                Author = "Jon Skeet",
                Publisher = "Manning",
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
                var filtered = _products
                    .Where(p => p.Type.Equals(productType, StringComparison.OrdinalIgnoreCase))
                    .ToList();

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

        /// <summary>
        /// Because our new ProductViewModel no longer has an ID,
        /// we'll just return null. If you still need an ID, consider
        /// adding a "ProductId" property back into your view model.
        /// </summary>
        public Task<ProductViewModel> GetProductByIdAsync(long productId)
        {
            Log.Verbose("Mock: Called GetProductByIdAsync({ProductId}) but no ID field in the new model.", productId);
            // Return null or do any custom logic if you want to keep a hidden ID in memory.
            return Task.FromResult<ProductViewModel>(null);
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
                // Just add to the list (no actual ID to assign)
                _products.Add(product);

                Log.Information("Mock: Successfully added product: {Name} (Type={Type})", product.Name, product.Type);
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
