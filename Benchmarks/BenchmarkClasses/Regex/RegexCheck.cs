using BenchmarkDotNet.Attributes;
using System.Text.RegularExpressions;

namespace Benchmarks.BenchmarkClasses.RegexCheck;

[MemoryDiagnoser]
public partial class RegexCheck
{
    private const string email = "test@intellitect.com";

    [Benchmark]
    public bool PatternPass_IsMatch() => Regex.IsMatch(email,
                       @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                       RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));

    [Benchmark]
    public bool GeneratedRegex_IsMatch()
    {
        return EmailRegex().IsMatch(email);
    }
}
partial class RegexCheck
{
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase)]
    public static partial Regex EmailRegex();
}
