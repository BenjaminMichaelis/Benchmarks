using BenchmarkDotNet.Attributes;
using System.Text.RegularExpressions;

namespace Benchmarks.BenchmarkClasses.RegexBenchmarks;

[MemoryDiagnoser]
public partial class Regex_IsMatch
{
    private const string email = "test@intellitect.com";

    [Benchmark]
    public bool IsMatchMethod_PassPattern() => Regex.IsMatch(email,
                       @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                       RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));

    [Benchmark]
    public bool GeneratedRegex_IsMatchMethod()
    {
        return EmailRegex().IsMatch(email);
    }
}
partial class Regex_IsMatch
{
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase)]
    public static partial Regex EmailRegex();
}
