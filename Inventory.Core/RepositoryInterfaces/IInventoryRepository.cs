namespace Inventory.Core.RepositoryInterfaces
{
    public interface IInventoryRepository
    {
        /// <summary>
        /// If row doesn't exist, create it; otherwise update.
        /// </summary>
        Task<int> CreateOrUpdateInventoryAsync(int productId, int depotId, int quantity);

        /// <summary>
        /// Gets the quantity in a specific depot for a product.
        /// </summary>
        Task<int> GetQuantityAsync(int productId, int depotId);

        /// <summary>
        /// Gets the sum across all depots for a product.
        /// (If you want to fill the Product.Amount property with total stock).
        /// </summary>
        Task<int> GetTotalQuantityForProductAsync(int productId);
    }
}

//public interface IInventoryRepository
//{
//    int GetProductQuantityAsync(string productId);
//}