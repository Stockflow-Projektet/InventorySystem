using Inventory.Core.Models.Abstracts;

namespace Inventory.API.RepositoryImplementations;
/*
public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddOrderAsync(IOrder order)
    {
        await _context.orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<IOrder>> GetOrdersAsync()
    {
        return await _context.orders.ToListAsync();
    }

    public async Task<IOrder> GetOrderByIdAsync(int id)
    {
        return await _context.orders.FindAsync(id);
    }

    public async Task<IEnumerable<IOrder>> QueryOrdersAsync(string query)
    {
        return await _context.orders
            .Where(p => p.Name.Contains(query) || p.Description.Contains(query))
            .ToListAsync();
    }

    public async Task UpdateOrderAsync(IOrder order)
    {
        _context.orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrderAsync(int id)
    {
        var order = await _context.orders.FindAsync(id);
        if (order != null)
        {
            _context.orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
*/