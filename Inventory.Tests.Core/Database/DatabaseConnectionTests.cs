using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Serilog;
using Serilog.Events;
using Inventory.Core.Database;

namespace Inventory.Tests.Core.DatabaseTests
{
    public class DatabaseConnectionTests
    {
        private const string FakeConnectionString = "FAKE_CONNECTION_STRING";

        static DatabaseConnectionTests()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Verbose)
                .CreateLogger();
        }

        [Fact]
        public void NotInitialized_ShouldThrow()
        {
            // Reset everything
            DatabaseConnection.Reset();

            // Try to access Instance => Should throw
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                var inst = DatabaseConnection.Instance;
            });
            Assert.Contains("Initialize", ex.Message);
        }

        [Fact]
        public void Instance_ReturnsSameObject_EveryTime()
        {
            // Reset
            DatabaseConnection.Reset();

            // Initialize with a fake factory
            DatabaseConnection.Initialize(FakeConnectionString, new FakeConnectionFactory());

            var a = DatabaseConnection.Instance;
            var b = DatabaseConnection.Instance;
            Assert.Same(a, b); // same instance
        }

        [Fact]
        public void MultipleThreads_AccessingSingleton_ShouldReturnSameInstance()
        {
            DatabaseConnection.Reset();
            DatabaseConnection.Initialize(FakeConnectionString, new FakeConnectionFactory());

            var instances = new DatabaseConnection[20];

            Parallel.For(0, 20, i =>
            {
                instances[i] = DatabaseConnection.Instance;
            });

            var first = instances[0];
            Assert.True(instances.All(x => x == first));
        }

        [Fact]
        public void CreatingMultipleConnections_ReturnsNewFakeConnections()
        {
            DatabaseConnection.Reset();
            DatabaseConnection.Initialize(FakeConnectionString, new FakeConnectionFactory());

            var dbConn = DatabaseConnection.Instance;
            var conn1 = dbConn.CreateSqlConnection();
            var conn2 = dbConn.CreateSqlConnection();

            // Should be distinct IDbConnection objects
            Assert.NotSame(conn1, conn2);
            // But both have same fake connection string
            Assert.Equal(FakeConnectionString, conn1.ConnectionString);
            Assert.Equal(FakeConnectionString, conn2.ConnectionString);
        }

        [Fact]
        public void ReInitialize_WithDifferentString_ShouldUpdate()
        {
            DatabaseConnection.Reset();
            // First init with a fake
            DatabaseConnection.Initialize(FakeConnectionString, new FakeConnectionFactory());

            // Re-init with a new string
            var newConnStr = "DIFFERENT_FAKE_STRING";
            DatabaseConnection.Initialize(newConnStr, new FakeConnectionFactory());

            // new instance is forced once we call .Instance (the old Lazy is cleared by .Reset? 
            // Actually we didn't call Reset - so let's see how we want to handle re-init in real logic.
            var dbConn = DatabaseConnection.Instance;
            var sqlConn = dbConn.CreateSqlConnection();

            Assert.Equal(newConnStr, sqlConn.ConnectionString);
        }
    }
}
