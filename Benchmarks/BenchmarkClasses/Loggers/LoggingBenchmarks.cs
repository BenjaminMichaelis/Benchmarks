using BenchmarkDotNet.Attributes;
using Benchmarks.BenchmarkClasses.Loggers.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BenjaminMichaelis.Benchmarks.LoggerBenchmarks;

[MemoryDiagnoser]
public class NoParamLoggingBenchmarks
{
    private ClassUsingStandardLogging _sut1;
    private ClassUsingOptimizedLogging _sut2;

    [GlobalSetup]
    public void Setup()
    {
        IServiceCollection serviceCollection = new ServiceCollection();

        serviceCollection.AddLogging(builder => builder
            .AddFilter("LoggingBenchmarks", LogLevel.Information)
        );

        var loggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();

        var logger = loggerFactory.CreateLogger("TEST");

        _sut1 = new ClassUsingStandardLogging(logger);
        _sut2 = new ClassUsingOptimizedLogging(logger);
    }

    [Benchmark(Baseline = true)]
    public void StandardLoggingNoParams() => _sut1.LogOnceWithNoParam();

    [Benchmark]
    public void OptimizedLoggingNoParams() => _sut2.LogOnceNoParams();
}

[MemoryDiagnoser]
public class OneParamLoggingBenchmarks
{
    private ClassUsingStandardLogging _sut1;
    private ClassUsingOptimizedLogging _sut2;
    private const string Value1 = "Value";

    [GlobalSetup]
    public void Setup()
    {
        IServiceCollection serviceCollection = new ServiceCollection();

        serviceCollection.AddLogging(builder => builder
            .AddFilter("LoggingBenchmarks", LogLevel.Information)
        );

        var loggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();

        var logger = loggerFactory.CreateLogger("TEST");

        _sut1 = new ClassUsingStandardLogging(logger);
        _sut2 = new ClassUsingOptimizedLogging(logger);
    }

    [Benchmark(Baseline = true)]
    public void StandardLoggingOneParam() => _sut1.LogOnceWithOneParam(Value1);

    [Benchmark]
    public void OptimizedLoggingOneParam() => _sut2.LogOnceOneParam(Value1);
}

[MemoryDiagnoser]
public class TwoParamLoggingBenchmarks
{
    private ClassUsingStandardLogging _sut1;
    private ClassUsingOptimizedLogging _sut2;

    private const string Value1 = "Value";
    private const int Value2 = 1000;

    [GlobalSetup]
    public void Setup()
    {
        IServiceCollection serviceCollection = new ServiceCollection();

        serviceCollection.AddLogging(builder => builder
            .AddFilter("LoggingBenchmarks", LogLevel.Information)
        );

        var loggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();

        var logger = loggerFactory.CreateLogger("TEST");

        _sut1 = new ClassUsingStandardLogging(logger);
        _sut2 = new ClassUsingOptimizedLogging(logger);
    }

    [Benchmark(Baseline = true)]
    public void StandardLoggingTwoParams() => _sut1.LogOnceWithTwoParams(Value1, Value2);

    [Benchmark]
    public void OptimizedLoggingTwoParams() => _sut2.LogOnceTwoParams(Value1, Value2);
}

[MemoryDiagnoser]
public class FilteredLoggingBenchmarks
{
    private ClassUsingStandardLogging _sut1;
    private ClassUsingOptimizedLogging _sut2;

    private const string Value1 = "Value";
    private const int Value2 = 1000;

    [GlobalSetup]
    public void Setup()
    {
        IServiceCollection serviceCollection = new ServiceCollection();

        serviceCollection.AddLogging(builder => builder
            .AddFilter("LoggingBenchmarks", LogLevel.Information)
        );

        var loggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();

        var logger = loggerFactory.CreateLogger("TEST");

        _sut1 = new ClassUsingStandardLogging(logger);
        _sut2 = new ClassUsingOptimizedLogging(logger);
    }

    [Benchmark(Baseline = true)]
    public void StandardLoggingDebug() => _sut1.LogDebugOnceWithTwoParams(Value1, Value2);

    [Benchmark]
    public void LogDebugWithoutLevelCheck() => _sut2.LogOnceUsingLoggerMessageWithoutLevelCheck(Value1, Value2);

    [Benchmark]
    public void LogDebugWithLevelCheck() => _sut2.LogOnceUsingLoggerMessageWithLevelCheck(Value1, Value2);
}
