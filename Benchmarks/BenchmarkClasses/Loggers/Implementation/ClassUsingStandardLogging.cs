using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging;

namespace Benchmarks.BenchmarkClasses.Loggers.Implementation;

[MemoryDiagnoser]
public class ClassUsingStandardLogging(ILogger logger)
{
    public void LogOnceWithNoParam() =>
        logger.LogInformation("This is a message with no params!");

#pragma warning disable CA2254 // Template should be a static expression
    public void LogOnceWithOneParam(string value1) =>
        logger.LogInformation($"This is a message with one param! {value1}");

    public void LogOnceWithTwoParams(string value1, int value2) =>
        logger.LogInformation($"This is a message with two params! {value1}, {value2}");

    public void LogDebugOnceWithTwoParams(string value1, int value2) =>
        logger.LogDebug($"This is a debug message with two params! {value1}, {value2}");
#pragma warning restore CA2254 // Template should be a static expression
}