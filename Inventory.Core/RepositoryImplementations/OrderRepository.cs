using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Serilog;
using Inventory.Core.Database;
using Inventory.Core.Models;
using Inventory.Core.Models.Abstracts;
using Inventory.Core.RepositoryInterfaces;

namespace Inventory.Core.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DatabaseConnection _db;

        public OrderRepository()
        {
            _db = DatabaseConnection.Instance;
        }

        public async Task<int> AddOrderAsync(IOrder order)
        {
            Log.Verbose("AddOrderAsync called for orderDate={Date}", order.OrderDate);

            const string insertOrderSql = @"
                INSERT INTO [dbo].[orders] ([orderDate])
                VALUES (@orderDate);
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";

            const string insertDetailSql = @"
                INSERT INTO [dbo].[orderDetails]
                    ([productId], [quantity], [orderId], [depotId])
                VALUES
                    (@ProductId, @Quantity, @OrderId, @DepotId);
            ";

            using IDbConnection conn = _db.CreateSqlConnection();
            try
            {
                await OpenConnectionAsync(conn);
                using var tran = conn.BeginTransaction();

                Log.Debug("Inserting into orders table...");
                var newId = await conn.ExecuteScalarAsync<int>(
                    insertOrderSql,
                    new { orderDate = order.OrderDate },
                    transaction: tran
                );
                order.OrderId = newId;

                if (order.OrderDetails != null && order.OrderDetails.Count > 0)
                {
                    foreach (var item in order.OrderDetails)
                    {
                        Log.Debug("Inserting order detail for productId={Prod}, depotId={Dep}",
                            item.ProductId, item.DepotId);

                        await conn.ExecuteAsync(
                            insertDetailSql,
                            new
                            {
                                item.ProductId,
                                item.Quantity,
                                OrderId = newId,
                                item.DepotId
                            },
                            transaction: tran
                        );
                    }
                }

                tran.Commit();
                Log.Information("Successfully inserted orderId={OrderId}", newId);
                return newId;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to insert order");
                throw;
            }
        }

        public async Task<IOrder> GetOrderByIdAsync(int orderId)
        {
            Log.Verbose("GetOrderByIdAsync called for orderId={Id}", orderId);

            const string orderSql = @"
                SELECT [orderId], [orderDate]
                FROM [dbo].[orders]
                WHERE [orderId] = @id;
            ";

            const string detailSql = @"
                SELECT [detailId] AS ItemId,
                       [productId],
                       [quantity],
                       [orderId],
                       [depotId]
                FROM [dbo].[orderDetails]
                WHERE [orderId] = @id;
            ";

            using IDbConnection conn = _db.CreateSqlConnection();
            try
            {
                await OpenConnectionAsync(conn);

                var orderRow = await conn.QuerySingleOrDefaultAsync<dynamic>(orderSql, new { id = orderId });
                if (orderRow == null)
                {
                    Log.Warning("No order found with orderId={Id}", orderId);
                    return null;
                }

                // Build an Order
                var order = new Order
                {
                    OrderId = orderRow.orderId,
                    OrderDate = orderRow.orderDate
                };

                // Query details
                var detailRows = await conn.QueryAsync<OrderDetail>(detailSql, new { id = orderId });
                order.OrderDetails = detailRows.Cast<IOrderDetail>().ToList();

                Log.Information("Fetched orderId={OrderId} with {Count} items", orderId, order.OrderDetails.Count);
                return order;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to retrieve orderId={Id}", orderId);
                throw;
            }
        }

        public async Task<IEnumerable<IOrder>> GetAllOrdersAsync()
        {
            Log.Verbose("GetAllOrdersAsync called.");

            const string ordersSql = @"
                SELECT [orderId], [orderDate]
                FROM [dbo].[orders];
            ";
            const string detailsSql = @"
                SELECT [detailId] AS ItemId,
                       [productId],
                       [quantity],
                       [orderId],
                       [depotId]
                FROM [dbo].[orderDetails];
            ";

            using IDbConnection conn = _db.CreateSqlConnection();
            try
            {
                await OpenConnectionAsync(conn);

                // Get main orders
                var ordersRows = (await conn.QueryAsync<dynamic>(ordersSql)).ToList();

                // Get all items
                var itemsRows = (await conn.QueryAsync<OrderDetail>(detailsSql)).ToList();

                var results = new List<IOrder>();
                foreach (var row in ordersRows)
                {
                    var o = new Order
                    {
                        OrderId = row.orderId,
                        OrderDate = row.orderDate,
                    };
                    // gather items
                    o.OrderDetails = itemsRows
                        .Where(x => x.OrderId == o.OrderId)
                        .Cast<IOrderDetail>()
                        .ToList();

                    results.Add(o);
                }

                Log.Information("Fetched {Count} orders from DB", results.Count);
                return results;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to retrieve all orders");
                throw;
            }
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            Log.Verbose("DeleteOrderAsync called for orderId={Id}", orderId);

            const string deleteDetailSql = @"
                DELETE FROM [dbo].[orderDetails]
                WHERE [orderId] = @id;
            ";
            const string deleteOrderSql = @"
                DELETE FROM [dbo].[orders]
                WHERE [orderId] = @id;
            ";

            using IDbConnection conn = _db.CreateSqlConnection();
            try
            {
                await OpenConnectionAsync(conn);
                using var tran = conn.BeginTransaction();

                await conn.ExecuteAsync(deleteDetailSql, new { id = orderId }, tran);
                await conn.ExecuteAsync(deleteOrderSql, new { id = orderId }, tran);

                tran.Commit();
                Log.Information("Deleted orderId={Id}", orderId);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to delete orderId={Id}", orderId);
                throw;
            }
        }

        // --------------------------------------------------------------
        // -------------------- Private Helper ---------------------------
        // --------------------------------------------------------------

        /// <summary>
        /// If underlying connection is DbConnection, do OpenAsync(), else do Open().
        /// </summary>
        private async Task OpenConnectionAsync(IDbConnection conn)
        {
            if (conn is DbConnection dbConn)
                await dbConn.OpenAsync();
            else
                conn.Open();
        }
    }
}
