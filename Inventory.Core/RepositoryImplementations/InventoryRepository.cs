using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Serilog;
using Inventory.Core.Database;
using Inventory.Core.RepositoryInterfaces;

namespace Inventory.Core.RepositoriesImplementations
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly DatabaseConnection _db;

        public InventoryRepository()
        {
            _db = DatabaseConnection.Instance;
        }

        public async Task<int> CreateOrUpdateInventoryAsync(int productId, int depotId, int quantity)
        {
            Log.Verbose("CreateOrUpdateInventoryAsync called. productId={Prod}, depotId={Dep}, qty={Qty}",
                productId, depotId, quantity);

            const string selectSql = @"
                SELECT [inventoryId]
                FROM [dbo].[Inventory]
                WHERE [productId] = @p AND [depotId] = @d;
            ";

            const string insertSql = @"
                INSERT INTO [dbo].[Inventory] ([productId], [depotId], [quantity])
                VALUES (@productId, @depotId, @quantity);
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";

            const string updateSql = @"
                UPDATE [dbo].[Inventory]
                SET [quantity] = @quantity
                WHERE [inventoryId] = @id;
            ";

            using IDbConnection conn = _db.CreateSqlConnection();
            try
            {
                // Open the connection in the same style as ProductRepository
                await OpenConnectionAsync(conn);

                var existingId = await conn.ExecuteScalarAsync<int?>(
                    selectSql,
                    new { p = productId, d = depotId }
                );

                if (existingId.HasValue)
                {
                    // Update existing record
                    await conn.ExecuteAsync(updateSql, new { id = existingId.Value, quantity });
                    Log.Information("Updated existing inventoryId={InvId} to quantity={Qty}",
                        existingId.Value, quantity);
                    return existingId.Value;
                }
                else
                {
                    // Insert new record
                    var newId = await conn.ExecuteScalarAsync<int>(
                        insertSql,
                        new { productId, depotId, quantity }
                    );
                    Log.Information("Created new inventory row with inventoryId={NewId}", newId);
                    return newId;
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Error in CreateOrUpdateInventoryAsync for productId={Prod}, depotId={Dep}",
                    productId, depotId);
                throw;
            }
        }

        public async Task<int> GetQuantityAsync(int productId, int depotId)
        {
            Log.Verbose("GetQuantityAsync called. productId={Prod}, depotId={Dep}", productId, depotId);

            const string sql = @"
                SELECT [quantity]
                FROM [dbo].[Inventory]
                WHERE [productId] = @p AND [depotId] = @d;
            ";

            using IDbConnection conn = _db.CreateSqlConnection();
            try
            {
                await OpenConnectionAsync(conn);

                var qty = await conn.ExecuteScalarAsync<int?>(sql, new { p = productId, d = depotId }) ?? 0;
                Log.Debug("Quantity for productId={Prod}, depotId={Dep} is {Qty}", productId, depotId, qty);
                return qty;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to get quantity for productId={Prod}, depotId={Dep}", productId, depotId);
                throw;
            }
        }

        public async Task<int> GetTotalQuantityForProductAsync(int productId)
        {
            Log.Verbose("GetTotalQuantityForProductAsync called. productId={Prod}", productId);

            const string sql = @"
                SELECT SUM([quantity])
                FROM [dbo].[Inventory]
                WHERE [productId] = @p;
            ";

            using IDbConnection conn = _db.CreateSqlConnection();
            try
            {
                await OpenConnectionAsync(conn);

                var total = await conn.ExecuteScalarAsync<int?>(sql, new { p = productId }) ?? 0;
                Log.Debug("Total quantity for productId={Prod} is {Total}", productId, total);
                return total;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to get total quantity for productId={Prod}", productId);
                throw;
            }
        }

        /// <summary>
        /// Matches the style from ProductRepository: attempts an async open if possible.
        /// </summary>
        private async Task OpenConnectionAsync(IDbConnection conn)
        {
            if (conn is DbConnection dbConn)
            {
                await dbConn.OpenAsync();
            }
            else
            {
                // Fallback for non-async connections
                conn.Open();
            }
        }
    }
}
