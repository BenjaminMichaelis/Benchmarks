using BenchmarkDotNet.Attributes;

namespace BenjaminMichaelis.Benchmarks;

public class NewListVsCollectionInitializer
{
    [Benchmark]
    public List<string> NewList() => new() { "a", "b", "c" };

    [Benchmark]
    public List<string> CollectionInitializedList() => ["a", "b", "c"];
}
