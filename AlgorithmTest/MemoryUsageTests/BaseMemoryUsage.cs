using AlgorithmTest.Helpers;
using AlgorithmTest.Models;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Session;

namespace AlgorithmTest.MemoryUsageTests;

/// <summary>
/// A memória használat mérését végző absztrakt osztály.
/// </summary>
internal abstract class BaseMemoryUsage
{
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
    public virtual void SetUp()
    {
        _session = new TraceEventSession("MathCryptMemorySession");

        _session.EnableProvider(
            ClrTraceEventParser.ProviderGuid,
            TraceEventLevel.Verbose,
            (ulong)ClrTraceEventParser.Keywords.GC
        );

        _session.Source.Clr.All += delegate (TraceEvent data)
        {
            if (data.EventName == "GC/AllocationTick")
            {
                Console.WriteLine($"AllocationTick Event: {data.PayloadString(0)} bytes");
            }
        };

        _processingTask = Task.Run(_session.Source.Process);
    }

    /// <summary>
    /// A teszteket lezáró függvény.
    /// </summary>
    public virtual void TearDown()
    {
        _session.Stop();
        _session.Dispose();

        if (_processingTask != null && !_processingTask.IsCompleted)
        {
            _processingTask.Wait();
            _processingTask.Dispose();
        }
    }

    /// <summary>
    /// A memória használat mérését végző függvény.
    /// </summary>
    /// <param name="algorithm">A tesztelendő algoritmus.</param>
    /// <param name="input">A titkosítandó szöveg.</param>
    protected static void MemoryUsage(IAlgorithm algorithm, string input)
    {
        long memoryBefore = GC.GetTotalMemory(forceFullCollection: true);

        string cipherText = algorithm.Encrypt(input);

        long memoryBetween = GC.GetTotalMemory(forceFullCollection: true);

        string plainText = algorithm.Decrypt(cipherText);

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
