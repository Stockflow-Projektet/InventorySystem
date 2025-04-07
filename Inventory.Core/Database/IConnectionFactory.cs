using System.Data;
using Microsoft.Data.SqlClient;

namespace Inventory.Core.Database
{
    /// <summary>
    /// Abstraction for creating database connections,
    /// enabling mocks for unit tests without a real DB.
    /// </summary>
    public interface IConnectionFactory
    {
        IDbConnection CreateConnection(string connectionString);
    }
}
