using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Engines;
using ShopDemo.Benchmarks.Interfaces;
using ShopDemo.SharedKernel.ValueObjects;

namespace ShopDemo.Benchmarks.SharedKernelBenchs.ValueObjectsBenchs.DateTimeValueObectBenchs;

[SimpleJob(RunStrategy.Throughput, launchCount: 1)]
[HardwareCounters(HardwareCounter.BranchMispredictions, HardwareCounter.BranchInstructions)]
[MemoryDiagnoser]
public class FromUtcNowtBenchmark
    : IBenchmark
{
    [Benchmark(Baseline = true, Description = "DateTime")]
    public DateTime GetUtcNowFromDateTime() 
        => DateTime.UtcNow;

    [Benchmark(Description = "DateTimeOffset")]
    public DateTimeOffset GetUtcNowFromDateTimeOffset()
        => DateTimeOffset.UtcNow;

    [Benchmark(Description = nameof(DateTimeValueObject))]
    public DateTimeOffset GetUtcNowFromDateTimeValueObject()
        => DateTimeValueObject.FromUtcNow();
}
