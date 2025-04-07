using Inventory.Core.Factories.Interfaces;
using Inventory.Core.Models;
using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Factories.Implementations;

public class BookFactory : IProductFactory
{
    public Product CreateProduct(string productDto)
    {
        // Here constraints on book creation are defined
        if (string.IsNullOrWhiteSpace(productDto)) throw new ArgumentException("Product name is required.");

        //if (productDto.Price <= 0) throw new ArgumentException("Price must be greater than 0.");

        // Create and return the product
        return new Book
        {
            Type = "Book",
            ProductId = 0,
            Name = productDto,
            Description = productDto,
            Price = productDto,
            Pages = 3,
            Author = productDto,
            Publisher = productDto,
            PublicationYear = 1996
        };
    }
}