using System.IO;
using System.Runtime.CompilerServices;

public static class Logger
{
    private static RSSFeedifyCommon.Services.LoggingService? _loggerService = null;

    public static void Initialize(RSSFeedifyCommon.Services.LoggingService loggerService)
    {
        _loggerService = loggerService;
    }

    private static Serilog.ILogger GetLoggerForContext([CallerFilePath] string callerFilePath = "")
    {
        string className = Path.GetFileNameWithoutExtension(callerFilePath);
        return _loggerService?.Logger.ForContext("SourceContext", className) ?? Serilog.Log.Logger;
    }

    // Automatically set context and log
    public static void LogDebug(string message, [CallerFilePath] string callerFilePath = "")
    {
        GetLoggerForContext(callerFilePath).Debug(message);
    }

    public static void LogInformation(string message, [CallerFilePath] string callerFilePath = "")
    {
        GetLoggerForContext(callerFilePath).Information(message);
    }

    public static void LogError(string message, [CallerFilePath] string callerFilePath = "")
    {
        GetLoggerForContext(callerFilePath).Error(message);
    }

    public static void LogFatal(string message, [CallerFilePath] string callerFilePath = "")
    {
        GetLoggerForContext(callerFilePath).Fatal(message);
    }
}
