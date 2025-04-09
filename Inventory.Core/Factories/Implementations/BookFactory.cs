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
            Pages = productCreationArgs.Pages,
            Author = productCreationArgs.Author,
            Publisher = productCreationArgs.Publisher,
            PublicationYear = productCreationArgs.PublicationYear
        };
    }
}