using AlgorithmTest.Helpers;
using AlgorithmTest.Models;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Session;

namespace AlgorithmTest.MemoryUsageTests;

/// <summary>
/// A memória használat mérését végző absztrakt osztály.
/// </summary>
[TestFixture]
[NonParallelizable]
internal abstract class BaseMemoryUsage<Algorithm>
    where Algorithm : IAlgorithm, new()
{
    /// <summary>
    /// A titkosító algoritmust tároló adattag.
    /// </summary>
    private IAlgorithm _algorithm;

    /// <summary>
    /// Az események feldolgozását jelző igéret.
    /// </summary>
    private Task<bool> _processingTask;

    /// <summary>
    /// Az eseményeket megfigyelő adattag.
    /// </summary>
    private TraceEventSession _session;

    /// <summary>
    /// A teszteket előkészítő függvény.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _algorithm = new Algorithm();

        _session = new TraceEventSession($"MemorySession-{Guid.NewGuid()}");

        _session.EnableProvider(
            ClrTraceEventParser.ProviderGuid,
            TraceEventLevel.Verbose,
            (ulong)ClrTraceEventParser.Keywords.GC
        );

        _session.Source.Clr.All += data =>
        {
            if (data.EventName.Equals("GC/AllocationTick"))
            {
                Console.WriteLine($"AllocationTick Event: {data.PayloadString(0)} bytes");
            }
        };

        _processingTask = Task.Run(_session.Source.Process);
    }

    /// <summary>
    /// A teszteket lezáró függvény.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _session.Stop();
        _session.Dispose();

        if (_processingTask != null && !_processingTask.IsCompleted)
        {
            _processingTask.Wait();
            _processingTask.Dispose();
        }

        _algorithm.Dispose();
    }

    /// <summary>
    /// A memória használat mérését végző függvény.
    /// </summary>
    /// <param name="input">A titkosítandó szöveg.</param>
    protected void MemoryUsage(string input)
    {
        long memoryBefore = GC.GetTotalMemory(forceFullCollection: true);

        string cipherText = _algorithm.Encrypt(input);

        long memoryBetween = GC.GetTotalMemory(forceFullCollection: true);

        string plainText = _algorithm.Decrypt(cipherText);

        long memoryAfter = GC.GetTotalMemory(forceFullCollection: true);

        TestContext.Out.WriteLine(
            StringHelper.MemoryUsageWhileEncrypt(memoryBetween - memoryBefore)
        );

        TestContext.Out.WriteLine(
            StringHelper.MemoryUsageWhileDecrypt(memoryAfter - memoryBetween)
        );

        Assert.That(plainText, Is.EqualTo(input));
    }
}
