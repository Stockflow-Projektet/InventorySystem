namespace Inventory.Core.Repositories;

public interface IInventoryRepository
{
    int GetProductQuantityAsync(string productId);
}