using BenchmarkDotNet.Attributes;

namespace BenjaminMichaelis.Benchmarks.ConditionalsBenchmarks;

[MemoryDiagnoser]
public class ToListVsRangeOperator
{
    public IEnumerable<string> OriginalList = ["a", "b", "c"];

    [Benchmark]
    public List<string> ToList() => OriginalList.ToList();

    [Benchmark]
    public List<string> RangeOperator() => [.. OriginalList];
}
