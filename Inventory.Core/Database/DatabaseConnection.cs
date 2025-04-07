using Serilog;
using Microsoft.Data.SqlClient;
using System;

namespace Inventory.Core.Database
{
    /// <summary>
    /// Singleton for retrieving database connections. 
    /// The connection string must be initialized from outside 
    /// (e.g., Inventory.Frontend).
    /// </summary>
    public sealed class DatabaseConnection
    {
        private static readonly Lazy<DatabaseConnection> _instance =
            new Lazy<DatabaseConnection>(() => new DatabaseConnection());

        /// <summary>
        /// Stored connection string from the outside world.
        /// </summary>
        private static string _externalConnectionString;

        private DatabaseConnection()
        {
            // We assume logging is already configured in the composition root.
            Log.Verbose("DatabaseConnection singleton instantiated.");
        }

        /// <summary>
        /// Must be called once before using Instance.
        /// If not called, Instance will throw an InvalidOperationException.
        /// </summary>
        public static void Initialize(string connectionString)
        {
            // This sets the connection string from the main project
            _externalConnectionString = connectionString;
            Log.Information("DatabaseConnection initialized with external connection string.");
        }

        /// <summary>
        /// The global instance of DatabaseConnection.
        /// </summary>
        public static DatabaseConnection Instance
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_externalConnectionString))
                {
                    Log.Fatal("DatabaseConnection is accessed before calling Initialize().");
                    throw new InvalidOperationException(
                        "DatabaseConnection.Initialize(...) must be called before accessing Instance."
                    );
                }
                return _instance.Value;
            }
        }

        /// <summary>
        /// Creates and returns a new SqlConnection using 
        /// the externally provided connection string.
        /// </summary>
        public SqlConnection CreateSqlConnection()
        {
            try
            {
                Log.Debug("Creating a new SqlConnection.");
                var conn = new SqlConnection(_externalConnectionString);
                return conn;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to create SqlConnection.");
                throw;
            }
        }
    }
}
