using Inventory.Core.Models;

namespace Inventory.Core.Factories;

public class BookFactory : IOrderFactory
{
    
    public Product CreateProduct(string title, string description, decimal price, int )
    {
        Product book = new Book();
        book.ProductType = productType;
    }
}