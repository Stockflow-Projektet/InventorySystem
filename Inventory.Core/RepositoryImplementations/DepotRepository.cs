using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Serilog;
using Inventory.Core.Database;
using Inventory.Core.RepositoryInterfaces;

namespace Inventory.Core.Repositories
{
    public class DepotRepository : IDepotRepository
    {
        private readonly DatabaseConnection _db;

        public DepotRepository()
        {
            _db = DatabaseConnection.Instance;
        }

        public async Task<int> AddDepotAsync(string depotName)
        {
            Log.Verbose("AddDepotAsync: name={Name}", depotName);

            const string sql = @"
                INSERT INTO [dbo].[depots] ([name])
                VALUES (@name);
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";

            using IDbConnection conn = _db.CreateSqlConnection();
            try
            {
                await OpenConnectionAsync(conn);

                var newId = await conn.ExecuteScalarAsync<int>(sql, new { name = depotName });
                Log.Information("Created depotId={Id} with name={Name}", newId, depotName);
                return newId;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to add depot with name={Name}", depotName);
                throw;
            }
        }

        public async Task<IEnumerable<(int depotId, string name)>> GetAllDepotsAsync()
        {
            Log.Verbose("GetAllDepotsAsync called.");

            const string sql = @"
                SELECT [depotId], [name]
                FROM [dbo].[depots];
            ";

            using IDbConnection conn = _db.CreateSqlConnection();
            try
            {
                await OpenConnectionAsync(conn);
                // We'll read into anonymous objects then map:
                var rows = await conn.QueryAsync(sql);

                var results = new List<(int depotId, string name)>();
                foreach (var row in rows)
                {
                    int dId = row.depotId;
                    string nm = row.name;
                    results.Add((dId, nm));
                }

                Log.Information("Retrieved {Count} depots.", results.Count);
                return results;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to get all depots");
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
