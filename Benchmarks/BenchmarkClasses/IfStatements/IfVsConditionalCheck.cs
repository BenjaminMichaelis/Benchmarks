using BenchmarkDotNet.Attributes;

namespace BenjaminMichaelis.Benchmarks.Initializations;

[MemoryDiagnoser]
public class IfVsConditionalCheck
{
    private readonly List<string>? nonNullList = [];

    [Benchmark]
    public List<string>? IfNullCheck_NonNull()
    {
        if (nonNullList is null)
        {
            throw new Exception("We won't hit this");
        }

        return nonNullList;
    }

    [Benchmark]
    public List<string>? IfConditionalNullCheck_NonNull()
    {
        return nonNullList is null
            ? throw new Exception("We won't hit this")
            : nonNullList;
    }
}
