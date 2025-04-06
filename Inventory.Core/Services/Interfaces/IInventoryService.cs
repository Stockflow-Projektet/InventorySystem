namespace Inventory.Core.Services.Interfaces;

public interface IInventoryService
{
    int GetProductQuantity(int productId);
}