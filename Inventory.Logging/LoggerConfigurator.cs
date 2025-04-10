using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Inventory.Logging
{
    public static class LoggerConfigurator
    {
        private static Logger _logger;
        private static string _runTimestamp;
        private static bool _isConfigured;
        private static string _configuredProjectName;

        /// <summary>
        /// Configures a single, global logger. If the logger has already been configured,
        /// then the call is ignored and a warning is written.
        /// </summary>
        /// <param name="projectName">The project name requesting configuration (e.g., "Inventory.API").</param>
        public static void ConfigureLogger(string projectName)
        {
            if (_isConfigured)
            {
                // Optionally, log a debug message or write to Console that the logger is already configured.
                if (!projectName.Equals(_configuredProjectName, System.StringComparison.OrdinalIgnoreCase))
                {
                    // If a different project name is passed, you can log a message or simply ignore the new value.
                    Log.Debug("Logger already configured with project '{ConfiguredProjectName}'. Ignoring configuration request for project '{RequestedProjectName}'.",
                        _configuredProjectName, projectName);
                }
                return;
            }

            // Use the provided project name for initial configuration.
            _configuredProjectName = projectName;
            _runTimestamp = System.DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss");

            // Get the STARTUP project's root. Since Inventory.Core is likely running inside the API host, 
            // this will point to the API project location.
            var startupAssemblyPath = System.AppContext.BaseDirectory;
            var projectRoot = System.IO.Path.GetFullPath(System.IO.Path.Combine(startupAssemblyPath, @"..\..\.."));

            var logsRoot = System.IO.Path.Combine(projectRoot, "Logs");
            var baseLogsPath = System.IO.Path.Combine(logsRoot, _configuredProjectName, _runTimestamp);

            System.IO.Directory.CreateDirectory(baseLogsPath);

            var outputTemplate =
                $"[{_configuredProjectName}] {{Timestamp:yyyy-MM-dd HH:mm:ss.fff}} [{{Level:u3}}] {{Message:lj}}{{NewLine}}{{Exception}}";

            var loggerConfig = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Verbose)
                    .WriteTo.File(
                        System.IO.Path.Combine(baseLogsPath, "trace.log"),
                        outputTemplate: outputTemplate,
                        rollingInterval: RollingInterval.Infinite,
                        shared: true
                    ))
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Debug)
                    .WriteTo.File(
                        System.IO.Path.Combine(baseLogsPath, "debug.log"),
                        outputTemplate: outputTemplate,
                        rollingInterval: RollingInterval.Infinite,
                        shared: true
                    ))
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
                    .WriteTo.File(
                        System.IO.Path.Combine(baseLogsPath, "info.log"),
                        outputTemplate: outputTemplate,
                        rollingInterval: RollingInterval.Infinite,
                        shared: true
                    ))
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
                    .WriteTo.File(
                        System.IO.Path.Combine(baseLogsPath, "warn.log"),
                        outputTemplate: outputTemplate,
                        rollingInterval: RollingInterval.Infinite,
                        shared: true
                    ))
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal)
                    .WriteTo.File(
                        System.IO.Path.Combine(baseLogsPath, "fatal.log"),
                        outputTemplate: outputTemplate,
                        rollingInterval: RollingInterval.Infinite,
                        shared: true
                    ))
                .WriteTo.File(
                    System.IO.Path.Combine(baseLogsPath, "combined.log"),
                    outputTemplate: outputTemplate,
                    restrictedToMinimumLevel: LogEventLevel.Verbose,
                    rollingInterval: RollingInterval.Infinite,
                    shared: true
                );

            _logger = loggerConfig.CreateLogger();
            Log.Logger = _logger;
            _isConfigured = true;
        }
    }
}
