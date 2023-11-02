using System.Diagnostics;

namespace _03_plinq_tests;

public class PLinqTests
{
    private const int Max = 100000000;
    private static readonly int[] Values;
    private Stopwatch _stopWatch = null!;

    [SetUp]
    public void Init()
    {
        _stopWatch = new Stopwatch();
        _stopWatch.Start();
    }

    [TearDown]
    public void Cleanup()
    {
        _stopWatch.Stop();
        Console.WriteLine(_stopWatch.ElapsedMilliseconds);
    }

    static PLinqTests()
    {
        Values = GetRandomArray();
    }

    private static int[] GetRandomArray()
    {
        var random = new Random();
        return Enumerable.Range(1, Max)
            .Select(x => random.Next(1, Max))
            .ToArray();
    }

    [Test]
    public void MaxTest()
    {
        _ = Values.Max();
    }

    [Test]
    public void AvgTest()
    {
        _ = Values.Average();
    }

    [Test]
    public void MaxAsParallelTest()
    {
        _ = Values.AsParallel().Max();
    }

    [Test]
    public void AvgAsParallelTest()
    {
        _ = Values.AsParallel().Average();
    }

    [Test]
    public void PrimeNumbersTest()
    {
        // Calculate prime numbers using a simple (unoptimized) algorithm.
        // This calculates prime numbers between 3 and a million, using all available cores:

        var numbers = Enumerable.Range(3, 1000000 - 3);

        var parallelQuery = numbers
            //.AsParallel()
            .Where(n => Enumerable.Range(2, (int) Math.Sqrt(n)).All(i => n % i > 0))
            .OrderBy(p => p);

        var primes = parallelQuery.ToArray();
        var last = primes.Last();
        Assert.That(last, Is.EqualTo(999983));
    }
}