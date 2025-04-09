using Inventory.Core.Factories.Interfaces;
using Inventory.Core.Models;
using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Factories.Implementations;

public class WritingToolFactory : IProductFactory
{
    public string FactoryType => "wri";
    public Product CreateProduct(ProductCreationArgs productCreationArgs)
    {
        // Here constraints on writing tool creation are defined
        if (string.IsNullOrWhiteSpace(productCreationArgs.Name)) throw new ArgumentException("Product name is required.");

        if (productCreationArgs.Price <= 0) throw new ArgumentException("Price must be greater than 0.");

        // Create and return the product
        return new WritingImplements()
        {
            Type = "wri",
            ProductId = 0,
            Name = productCreationArgs.Name,
            Description = productCreationArgs.Description,
            Price = productCreationArgs.Price,
            InkColor = productCreationArgs.InkColor,
            InkType = productCreationArgs.InkType,
            TipSize = productCreationArgs.TipSize,
            Status = productCreationArgs.Status
        };
    }

    public Product UpdateProduct(Product existingProduct, ProductCreationArgs productUpdateargs)
    {
        throw new NotImplementedException();
    }
}