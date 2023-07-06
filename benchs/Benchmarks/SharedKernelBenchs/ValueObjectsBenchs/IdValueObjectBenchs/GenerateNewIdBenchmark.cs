using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Engines;
using ShopDemo.Benchmarks.Interfaces;
using ShopDemo.SharedKernel.ValueObjects;

namespace ShopDemo.Benchmarks.SharedKernelBenchs.ValueObjectsBenchs.IdValueObjectBenchs;

[SimpleJob(RunStrategy.Throughput, launchCount: 1)]
[HardwareCounters(HardwareCounter.BranchMispredictions, HardwareCounter.BranchInstructions)]
[MemoryDiagnoser]
public class GenerateNewIdBenchmark
    : IBenchmark
{
    [Benchmark(Baseline = true, Description = "System.Guid")]
    public Guid GenerateIdUsingSystemGuid()
        => Guid.NewGuid();

    [Benchmark(Description = nameof(IdValueObject))]
    public Guid GenerateIdUsingIdValueObject()
        => IdValueObject.GenerateNewId();
}
