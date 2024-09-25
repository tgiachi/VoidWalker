using Microsoft.Extensions.Logging;
using VoidWalker.Engine.Core.Attributes.Scripts;

namespace VoidWalker.Engine.Core.ScriptModules;

[ScriptModule]
public class LoggerModule
{
    private readonly ILogger _logger;

    public LoggerModule(ILogger<LoggerModule> logger)
    {
        _logger = logger;
    }


    [ScriptFunction("log_info", "Logs a message with the info level.")]
    public void Log(string message, params object[] args)
    {
        _logger.LogInformation(message, args);
    }


    [ScriptFunction("log_warning", "Logs a message with the warning level.")]
    public void LogWarning(string message, params object[] args)
    {
        _logger.LogWarning(message, args);
    }

    [ScriptFunction("log_error", "Logs a message with the error level.")]
    public void LogError(string message, params object[] args)
    {
        _logger.LogError(message, args);
    }

    [ScriptFunction("log_debug", "Logs a message with the debug level.")]
    public void LogDebug(string message, params object[] args)
    {
        _logger.LogDebug(message, args);
    }
}
