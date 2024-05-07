using BenchmarkDotNet.Attributes;
using Dia2Lib;
using System.Text;

namespace BenjaminMichaelis.Benchmarks.ConditionalsBenchmarks;

[MemoryDiagnoser]
public class TestJoinStringByDelimiterFunctions
{
    private static readonly string?[] strings = { "", "Value", null, "Hello", "", null, "", null, "Test", null };
    [Benchmark]
    public string JoinIgnoringEmptyStrings() => JoinIgnoringEmptyStrings(strings, ";");

    [Benchmark]
    public string JoinIgnoringEmptyStringsWithIndexer() => JoinIgnoringEmptyStringsWithIndexer(strings, ";");

    public static string JoinIgnoringEmptyStringsWithIndexer(string?[] strings, string delimiter)
    {
        StringBuilder result = new();
        if (strings.Length > 0)
        {
            result.Append(strings[0]);
            foreach (string? item in strings[..1])
            {
                if (!string.IsNullOrEmpty(item))
                {
                    result.Append(delimiter);
                    result.Append(item);
                }
            }
        }
        return result.ToString();
    }

    public static string JoinIgnoringEmptyStrings(string?[] strings, string delimiter)
    {
        StringBuilder result = new();
        foreach (string s in strings)
        {
            if (!string.IsNullOrEmpty(s))
            {
                if (result.Length > 0)
                {
                    result.Append(delimiter);
                }
                result.Append(s);
            }
        }
        return result.ToString();
    }
}
