namespace Inventory.Core.RepositoryInterfaces;

public interface IInventoryRepository
{
    int GetProductQuantityAsync(string productId);
}