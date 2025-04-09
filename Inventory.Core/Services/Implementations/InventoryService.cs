using Inventory.Core.RepositoryInterfaces;
using Inventory.Core.Services.Interfaces;

namespace Inventory.Core.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public int GetProductQuantity(int productId)
        {
            // Normally you'd want an async method here, but since
            // the interface is synchronous, we do .Result:
            return _inventoryRepository
                .GetTotalQuantityForProductAsync(productId)
                .GetAwaiter().GetResult();
        }
    }
}
