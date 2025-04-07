using System.Data;
using Inventory.Core.Database;

namespace Inventory.Tests.Core.DatabaseTests
{
    public class FakeConnectionFactory : IConnectionFactory
    {
        public IDbConnection CreateConnection(string connectionString)
        {
            return new FakeConnection(connectionString);
        }
    }

    public class FakeConnection : IDbConnection
    {
        public FakeConnection(string connStr)
        {
            ConnectionString = connStr;
        }

        public string ConnectionString { get; set; }
        public int ConnectionTimeout => 0;
        public string Database => "FakeDB";
        public ConnectionState State => ConnectionState.Closed;

        public IDbTransaction BeginTransaction() => null;
        public IDbTransaction BeginTransaction(IsolationLevel il) => null;
        public void ChangeDatabase(string databaseName) { }
        public void Close() { }
        public void Dispose() { }
        public void Open() { }
        public IDbCommand CreateCommand() => null;
    }
}
