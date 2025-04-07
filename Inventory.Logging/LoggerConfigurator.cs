namespace Inventory.Logging;

public static class LoggerConfigurator
{
    private static Logger _logger;
    private static string _runTimestamp;
    private static bool _isConfigured;
    private static string _projectName;

    public static void ConfigureLogger(string projectName)
    {
        if (_isConfigured)
            return;

        _projectName = projectName;
        _runTimestamp = DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss");

        // Get the STARTUP project's root
        var startupAssemblyPath = AppContext.BaseDirectory;
        var projectRoot = Path.GetFullPath(Path.Combine(startupAssemblyPath, @"..\..\.."));

        var logsRoot = Path.Combine(projectRoot, "Logs");
        var baseLogsPath = Path.Combine(logsRoot, projectName, _runTimestamp);

        Directory.CreateDirectory(baseLogsPath);

        var outputTemplate =
            $"[{projectName}] {{Timestamp:yyyy-MM-dd HH:mm:ss.fff}} [{{Level:u3}}] {{Message:lj}}{{NewLine}}{{Exception}}";

        var loggerConfig = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Verbose)
                .WriteTo.File(
                    Path.Combine(baseLogsPath, "trace.log"),
                    outputTemplate: outputTemplate,
                    rollingInterval: RollingInterval.Infinite,
                    shared: true
                ))
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Debug)
                .WriteTo.File(
                    Path.Combine(baseLogsPath, "debug.log"),
                    outputTemplate: outputTemplate,
                    rollingInterval: RollingInterval.Infinite,
                    shared: true
                ))
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
                .WriteTo.File(
                    Path.Combine(baseLogsPath, "info.log"),
                    outputTemplate: outputTemplate,
                    rollingInterval: RollingInterval.Infinite,
                    shared: true
                ))
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
                .WriteTo.File(
                    Path.Combine(baseLogsPath, "warn.log"),
                    outputTemplate: outputTemplate,
                    rollingInterval: RollingInterval.Infinite,
                    shared: true
                ))
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal)
                .WriteTo.File(
                    Path.Combine(baseLogsPath, "fatal.log"),
                    outputTemplate: outputTemplate,
                    rollingInterval: RollingInterval.Infinite,
                    shared: true
                ))
            .WriteTo.File(
                Path.Combine(baseLogsPath, "combined.log"),
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