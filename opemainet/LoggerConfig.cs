using Serilog.Sinks.SystemConsole.Themes;
using Serilog;

public static class LoggerConfig
{
    public static ILogger CreateLogger()
    {
        return new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                    .CreateLogger();
    }
}
