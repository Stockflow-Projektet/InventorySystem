using Inventory.Core.Database;
using Inventory.Core.Models.Abstracts;
using Inventory.Core.RepositoryInterfaces;

namespace Inventory.API.RepositoryImplementations;
/*
public class ProductRepository : IProductRepository
{
    private readonly DatabaseConnection _connection;

    public ProductRepository(DatabaseConnection connection)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection))
            ;
    }

    public void AddProductToDb(Product product)
    {
        _connection.Products.Add(product);
        _connection.SaveChanges();
    }

    public List<Product> GetAllProductsFromDb()
    {
        return _context.Products.ToList();
    }

    public Product GetProductByIdFromDb(int id)
    {
        return _context.Products.Find(id);
    }

    public List<Product> QueryProductsFromDb(string query)
    {
        return _context.Products
            .Where(p => p.Name.Contains(query) || p.Description.Contains(query))
            .ToListAsync();
    }

    public void UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChangesAsync();
    }

    public void DeleteAsync(int id)
    {
        var product = _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            _context.SaveChangesAsync();
        }
    }
}
*/