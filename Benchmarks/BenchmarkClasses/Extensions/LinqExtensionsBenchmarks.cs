using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Benchmarks.BenchmarkClasses.Extensions.Implementation;

namespace BenjaminMichaelis.Benchmarks.ExtensionsBenchmarks;

[MemoryDiagnoser]
public class LinqWhereNotNullBenchmarks
{
    private readonly List<string?> listWithSomeNullValues = ["think", null, "of", null, "the", null, "children", null];
    private readonly Consumer consumer = new();
    [Benchmark]
    public void WhereNotNull_WithWhereThenSelect()
    {
        listWithSomeNullValues.WhereNotNullWithLinqWhereThenSelect().Consume(consumer);
    }

    [Benchmark]
    public void WhereNotNull_WithWhereThenSelectCast()
    {
        listWithSomeNullValues.WhereNotNullWithLinqWhereThenSelectCast().Consume(consumer);
    }

    [Benchmark]
    public void WhereNotNull_WithIfCheck()
    {
        listWithSomeNullValues.WhereNotNullWithIfCheck().Consume(consumer);
    }

    [Benchmark]
    public void WhereNotNull_WithLinq()
    {
        listWithSomeNullValues.WhereNotNullWithLinqQueryExpression().Consume(consumer);
    }
}
