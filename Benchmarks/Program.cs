using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace BenjaminMichaelis.Benchmarks;

public class Program
{
    public static void Main(string[] args)
    {
        var config = DefaultConfig.Instance;
        _ = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);
    }    
}
