using System.Collections.Concurrent;
using log4net;
using Microsoft.Extensions.Logging;

namespace LCPE.Logging;

public class Log4NetProvider : ILoggerProvider
{
    private readonly ILog log;
    private readonly ConcurrentDictionary<string, Log4NetLoggerWrapper> _loggers =
        new ConcurrentDictionary<string, Log4NetLoggerWrapper>();

    public Log4NetProvider(ILog log)
    {
        this.log = log;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return _loggers.GetOrAdd(categoryName, CreateLoggerImplementation);
    }

    public void Dispose()
    {
        _loggers.Clear();
    }
    private Log4NetLoggerWrapper CreateLoggerImplementation(string name)
    {
        return new Log4NetLoggerWrapper(log);
    }
}