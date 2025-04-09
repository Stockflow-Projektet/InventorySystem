using System;
using System.Data;
using System.Data.Common;
using Serilog;

namespace Inventory.Core.Database
{
    /// <summary>
    /// A singleton for retrieving database connections. 
    /// Expects an external connection string and a factory.
    /// </summary>
    public sealed class DatabaseConnection
    {
        // Holds the user-provided connection string
        private static string _externalConnectionString;

        // Holds the factory (mock or real) for creating IDbConnections
        private static IConnectionFactory _factory;

        // Not readonly, so we can reset in tests if needed
        private static Lazy<DatabaseConnection> _instance;

        private readonly IConnectionFactory _connFactory;

        /// <summary>
        /// Private constructor uses whichever factory was set
        /// by the Initialize() call. 
        /// </summary>
        private DatabaseConnection(IConnectionFactory factory)
        {
            _connFactory = factory;
            Log.Verbose("DatabaseConnection singleton instantiated.");
        }

        /// <summary>
        /// Must be called once before using <see cref="Instance"/>.
        /// If not called, <see cref="Instance"/> throws an exception.
        /// </summary>
        /// <param name="connectionString">DB connection string.</param>
        /// <param name="factory">Optional factory (mock or real). Defaults to <see cref="RealSqlConnectionFactory"/>.</param>
        public static void Initialize(string connectionString, IConnectionFactory factory = null)
        {
            _externalConnectionString = connectionString;
            _factory = factory ?? new RealSqlConnectionFactory();

            Log.Information("DatabaseConnection initialized with external connection string.");
        }

        /// <summary>
        /// Access the global singleton. If <see cref="_instance"/> is null,
        /// we create it lazily at this moment using the current <see cref="_factory"/>.
        /// </summary>
        public static DatabaseConnection Instance
        {
            get
            {
                // Must have a valid connection string
                if (string.IsNullOrWhiteSpace(_externalConnectionString))
                {
                    Log.Fatal("DatabaseConnection accessed before calling Initialize().");
                    throw new InvalidOperationException("DatabaseConnection.Initialize(...) must be called first.");
                }

                // If not yet created, create it now
                if (_instance == null)
                {
                    _instance = new Lazy<DatabaseConnection>(() => new DatabaseConnection(_factory));
                }

                return _instance.Value;
            }
        }

        /// <summary>
        /// Creates and returns a new <see cref="DbConnection"/> using
        /// the externally provided connection string and the configured factory.
        /// </summary>
        public IDbConnection CreateSqlConnection()
        {
            try
            {
                Log.Debug("Creating a new DbConnection via the factory.");
                return _connFactory.CreateConnection(_externalConnectionString);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to create IDbConnection.");
                throw;
            }
        }

        /// <summary>
        /// Allows test code to reset the singleton state, so each test can
        /// start in a fresh environment.
        /// </summary>
        public static void Reset()
        {
            _externalConnectionString = null;
            _factory = null;
            _instance = null; // so next time we call Instance, it re-lazies
        }
    }
}
