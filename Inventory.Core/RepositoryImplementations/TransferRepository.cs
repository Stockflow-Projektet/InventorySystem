using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Serilog;
using Inventory.Core.Database;
using Inventory.Core.RepositoryInterfaces;

namespace Inventory.Core.Repositories
{
    public class TransferRepository : ITransferRepository
    {
        private readonly DatabaseConnection _db;
        private readonly IInventoryRepository _inventoryRepo;

        public TransferRepository(IInventoryRepository inventoryRepo)
        {
            _db = DatabaseConnection.Instance;
            _inventoryRepo = inventoryRepo;
        }

        public async Task<int> TransferProductAsync(int productId, int sendingDepotId, int receivingDepotId, int quantity)
        {
            Log.Verbose("TransferProductAsync: productId={Prod}, sending={Send}, receiving={Recv}, qty={Qty}",
                productId, sendingDepotId, receivingDepotId, quantity);

            const string insertTransferSql = @"
                INSERT INTO [dbo].[transfer]
                    ([quantity], [productId], [sendingDepotId], [receiverDepotId], [date])
                VALUES
                    (@quantity, @productId, @sendingDepotId, @receiverDepotId, GETDATE());
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";

            using IDbConnection conn = _db.CreateSqlConnection();
            try
            {
                await OpenConnectionAsync(conn);
                using var tran = conn.BeginTransaction();

                // Check sending quantity
                var sendingQty = await _inventoryRepo.GetQuantityAsync(productId, sendingDepotId);
                if (sendingQty < quantity)
                {
                    Log.Warning("Insufficient stock in sending depotId={Dep}. Available={Avail}, Needed={Qty}",
                                sendingDepotId, sendingQty, quantity);
                    throw new InvalidOperationException("Not enough stock to transfer.");
                }

                // Decrement from sending
                await _inventoryRepo.CreateOrUpdateInventoryAsync(productId, sendingDepotId, sendingQty - quantity);

                // Increment receiving
                var receivingQty = await _inventoryRepo.GetQuantityAsync(productId, receivingDepotId);
                await _inventoryRepo.CreateOrUpdateInventoryAsync(productId, receivingDepotId, receivingQty + quantity);

                // Log the transfer
                var newId = await conn.ExecuteScalarAsync<int>(
                    insertTransferSql,
                    new
                    {
                        quantity,
                        productId,
                        sendingDepotId,
                        receiverDepotId = receivingDepotId
                    },
                    transaction: tran
                );

                tran.Commit();
                Log.Information("TransferId={Id} completed for productId={Prod}, qty={Qty}", newId, productId, quantity);
                return newId;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to transfer productId={Prod}, qty={Qty}", productId, quantity);
                throw;
            }
        }

        // Helper for opening connection
        private async Task OpenConnectionAsync(IDbConnection conn)
        {
            if (conn is DbConnection dbConn)
            {
                await dbConn.OpenAsync();
            }
            else
            {
                conn.Open();
            }
        }
    }
}
