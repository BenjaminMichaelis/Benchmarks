using BenchmarkDotNet.Attributes;

namespace BenjaminMichaelis.Benchmarks.Initializations;

[MemoryDiagnoser]
public class CollectionInitializerVsNewListInitialization
{
    [Benchmark]
#pragma warning disable IDE0028 // Simplify collection initialization
    public List<string> NewList() => new() { "a", "b", "c" };
#pragma warning restore IDE0028 // Simplify collection initialization

    [Benchmark]
    public List<string> CollectionInitializedList() => ["a", "b", "c"];
}
