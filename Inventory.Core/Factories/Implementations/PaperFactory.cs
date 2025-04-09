using Inventory.Core.Factories.Interfaces;
using Inventory.Core.Models;
using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Factories.Implementations;

public class PaperFactory : IProductFactory
{
    public string FactoryType => "pap";
    public Product CreateProduct(ProductCreationArgs productCreationArgs)
    {
        // Here constraints on paper creation are defined
        if (string.IsNullOrWhiteSpace(productCreationArgs.Name)) throw new ArgumentException("Product name is required.");

        if (productCreationArgs.Price <= 0) throw new ArgumentException("Price must be greater than 0.");

        // Create and return the product
        return new Paper()
        {
            Type = "pap",
            ProductId = 0,
            Name = productCreationArgs.Name,
            Description = productCreationArgs.Description,
            Price = productCreationArgs.Price,
            PaperSize = productCreationArgs.PaperSize,
            PaperWeight = productCreationArgs.PaperWeight,
            CoatingType = productCreationArgs.CoatingType,
            Status = productCreationArgs.Status
        };
  }

    public Product UpdateProduct(Product existingProduct, ProductCreationArgs updatedProductData)
    {
        if (existingProduct == null)
        {
            throw new ArgumentNullException(nameof(existingProduct), "Existing product cannot be null.");
        }

        if (updatedProductData == null)
        {
            throw new ArgumentNullException(nameof(updatedProductData), "Updated product data cannot be null.");
        }

        var existingPaper = (Paper)existingProduct;

        // Apply updates from the new data to the existing product
        existingPaper.Type = "boo";
        existingPaper.Name = !string.IsNullOrWhiteSpace(updatedProductData.Name)
            ? updatedProductData.Name
            : existingPaper.Name;

        existingPaper.Description = !string.IsNullOrWhiteSpace(updatedProductData.Description)
            ? updatedProductData.Description
            : existingPaper.Description;

        existingPaper.Price = updatedProductData.Price > 0 ? updatedProductData.Price : existingPaper.Price;
        //existingPaper.Pages = updatedProductData.Pages;
        //existingPaper.Author = !string.IsNullOrWhiteSpace(updatedProductData.Author) ? updatedProductData.Author : existingPaper.Author;
        //existingPaper.Publisher = !string.IsNullOrWhiteSpace(updatedProductData.Publisher) ? updatedProductData.Publisher : existingPaper.Publisher;
        //existingPaper.PublicationYear = updatedProductData.PublicationYear;

        // Return updated product
        return existingProduct;
    }
}