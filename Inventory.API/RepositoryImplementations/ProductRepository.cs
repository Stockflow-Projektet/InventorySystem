using Inventory.Core.Models.Abstracts;

namespace Inventory.API.RepositoryImplementations;
/*
public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddProductToDb(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
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