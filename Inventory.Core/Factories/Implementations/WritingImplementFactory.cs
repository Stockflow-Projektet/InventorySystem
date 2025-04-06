using Inventory.Core.Factories.Interfaces;

namespace Inventory.Core.Factories.Implementations;

public class WritingImplementFactory : IProductFactory
{
    public Product CreateProduct(ProductDto productDto)
    {
        // Here constraints on writing tool creation are defined
        if (string.IsNullOrWhiteSpace(productDto.name))
        {
            throw new ArgumentException("Product name is required.");
        }

        if (productDto.price <= 0)
        {
            throw new ArgumentException("Price must be greater than 0.");
        }
        
        // Create and return the product
        return new Book
        { 
            Type = "Book", 
            ProductId = 0,
            Name = ProductDto.name,
            Description = ProductDto.description,
            Price = ProductDto.price,
            Pages = ProductDto.pages,
            Author = ProductDto.author,
            Publisher = ProductDto.publisher,
            PublicationYear = ProductDto.publicationYear
        };
    }
}