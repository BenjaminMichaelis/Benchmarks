using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging;

namespace BenjaminMichaelis.Benchmarks.LoggerBenchmarks;

[MemoryDiagnoser]
public class LoggerVsLoggerMessageLogging
{
    private class TestLogProvider : ILoggerProvider
    {
        public void Dispose()
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new TestLogger();
        }

        public class TestLogger : ILogger
        {
            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                var message = formatter(state, exception);

                if (!string.IsNullOrEmpty(message) && exception != null)
                {
                    throw new NotImplementedException();
                }
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                // This return should be NullScope.Instance, but it is internal for now
                //return NullScope.Instance
                return default;
            }

        }

    }

    private readonly LoggerFactory _factory;
    private readonly ILogger _logger;
    private readonly Random _random;
    private readonly Action<ILogger, Guid, int, double, Exception> _logMessage;

    public LoggerVsLoggerMessageLogging()
    {
        _factory = new LoggerFactory();
        _factory.AddProvider(new TestLogProvider());
        _logger = _factory.CreateLogger("benchmark");

        _logMessage
            = LoggerMessage.Define<Guid, int, double>(LogLevel.Information, new EventId(0),
            "CorrelationID {CorrelationId}, Arg1: {Arg1}, Arg2: {Arg2}");

        _random = new Random();
    }

    [Benchmark]
    public void LogDirectly()
    {
        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.Log(LogLevel.Information, "CorrelationID {firstGuid}, Arg1: {randomNext}, Arg2: {randomDouble}", Guid.NewGuid(), _random.Next(), _random.NextDouble());
        }
    }

    [Benchmark]
    public void LogFormat()
    {
        if (_logger.IsEnabled(LogLevel.Information))
        {
            string msg =
                $"CorrelationID {Guid.NewGuid()}, Arg1: {_random.Next()}, Arg2: {_random.NextDouble()}";
            _logger.Log(LogLevel.Information, 0, msg, null, Format);
        }

        string? Format(object state, Exception ex)
        {
            return state.ToString();
        }
    }

    [Benchmark]
    public void LogLoggerMessage()
    {
        _logMessage(_logger, Guid.NewGuid(), _random.Next(), _random.NextDouble(), null);
    }

    [Benchmark]
    public void LogLoggerInterpolatedStringMessage()
    {
#pragma warning disable CA2254 // Template should be a static expression
        _logger.LogInformation($"CorrelationID {Guid.NewGuid()}, Arg1: {_random.Next()}, Arg2: {_random.NextDouble()}, Arg3: {null}");
#pragma warning restore CA2254 // Template should be a static expression
    }

    [Benchmark]
    public void LogLoggerInterpolatedStringMessageDirectly()
    {
        _logger.LogInformation("CorrelationID {firstGuid}, Arg1: {randomNext}, Arg2: {randomDouble}, Arg3: {null}", Guid.NewGuid(), _random.Next(), _random.NextDouble(), null);
    }
}

