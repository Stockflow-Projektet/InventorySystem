using System.Data;
using Microsoft.Data.SqlClient;

namespace Inventory.Core.Database
{
    /// <summary>
    /// Default real implementation that creates SqlConnections
    /// for production usage.
    /// </summary>
    public class RealSqlConnectionFactory : IConnectionFactory
    {
        public IDbConnection CreateConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
