using Inventory.Core.Factories.Interfaces;
using Inventory.Core.Models;
using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Factories.Implementations;

public class BookFactory : IProductFactory
{
    public string FactoryType => "boo";

    public Product CreateProduct(ProductCreationArgs productCreationArgs)
    {
        // Here constraints on book creation are defined
        if (string.IsNullOrWhiteSpace(productCreationArgs.Name)) throw new ArgumentException("Product name is required.");

        if (productCreationArgs.Price <= 0) throw new ArgumentException("Price must be greater than 0.");

        // Create and return the product
        return new Book
        {
            Type = "boo",
            ProductId = 0,
            Name = productCreationArgs.Name,
            Description = productCreationArgs.Description,
            Price = productCreationArgs.Price,
            NumberOfPages = productCreationArgs.Pages,
            Author = productCreationArgs.Author,
            Publisher = productCreationArgs.Publisher,
            PublicationYear = productCreationArgs.PublicationYear
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
        
        var existingBook = (Book)existingProduct;
        
        // Apply updates from the new data to the existing product
        existingBook.Type = "boo";
        existingBook.Name = !string.IsNullOrWhiteSpace(updatedProductData.Name) ? updatedProductData.Name : existingBook.Name;

        existingBook.Description = !string.IsNullOrWhiteSpace(updatedProductData.Description) ? updatedProductData.Description : existingBook.Description;

        existingBook.Price = updatedProductData.Price > 0 ? updatedProductData.Price : existingBook.Price;
        existingBook.NumberOfPages = updatedProductData.Pages;
        existingBook.Author = !string.IsNullOrWhiteSpace(updatedProductData.Author) ? updatedProductData.Author : existingBook.Author;
        existingBook.Publisher = !string.IsNullOrWhiteSpace(updatedProductData.Publisher) ? updatedProductData.Publisher : existingBook.Publisher;
        existingBook.PublicationYear = updatedProductData.PublicationYear;

        // Return updated product
        return existingProduct;
    }
}