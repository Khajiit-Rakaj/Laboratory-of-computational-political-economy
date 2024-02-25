using log4net;
using Microsoft.Extensions.Logging;

namespace LCPE.Logging;

// https://dotnetthoughts.net/how-to-use-log4net-with-aspnetcore-for-logging/
public class Log4NetLoggerWrapper : ILogger
{
    private readonly ILog log;

    public Log4NetLoggerWrapper(ILog log)
    {
        this.log = log;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        switch (logLevel)
        {
            case LogLevel.Critical:
                return log.IsFatalEnabled;
            case LogLevel.Debug:
            case LogLevel.Trace:
                return log.IsDebugEnabled;
            case LogLevel.Error:
                return log.IsErrorEnabled;
            case LogLevel.Information:
                return log.IsInfoEnabled;
            case LogLevel.Warning:
                return log.IsWarnEnabled;
            default:
                throw new ArgumentOutOfRangeException(nameof(logLevel));
        }
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
        Exception exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        if (formatter == null)
        {
            throw new ArgumentNullException(nameof(formatter));
        }

        string message = null;
        if (null != formatter)
        {
            message = formatter(state, exception);
        }

        if (!string.IsNullOrEmpty(message) || exception != null)
        {
            switch (logLevel)
            {
                case LogLevel.Critical:
                    log.Fatal(message);
                    break;
                case LogLevel.Debug:
                case LogLevel.Trace:
                    log.Debug(message);
                    break;
                case LogLevel.Error:
                    log.Error(message);
                    break;
                case LogLevel.Information:
                    log.Info(message);
                    break;
                case LogLevel.Warning:
                    log.Warn(message);
                    break;
                default:
                    log.Warn($"Encountered unknown log level {logLevel}, writing out as Info.");
                    log.Info(message, exception);
                    break;
            }
        }
    }
}