using AlgorithmTest.Models;
using MathCrypt.Enums;
using MathCrypt.Services;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Session;
using AlgorithmTest.Helpers;

namespace AlgorithmTest;

/// <summary>
/// A MathCrypt titkosító algoritmus memória használatát vizsgáló tesztesetek.
/// </summary>
[TestFixture]
[NonParallelizable]
internal class MathCryptMemoryUsageTests
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
    /// A MathCrypt titkosító algoritmust tároló adattag.
    /// </summary>
    private MathCryptAlgorithm _mathCrpyt;

    /// <summary>
    /// A teszteket előkészítő függvény.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        char[][] key = KeyGenService.Instance.GenerateKey(
            strength: 2,
            ECharset.Space,
            ECharset.Numbers,
            ECharset.MathSymbols,
            ECharset.Punctuations,
            ECharset.EN,
            ECharset.HU
        );

        _mathCrpyt = new(key);

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
    }

    /// <summary>
    /// Az egyszerű tesztesetek memória használatát vizsgáló teszt.
    /// </summary>
    /// <param name="input">A titkosítandó szöveg.</param>
    [Test, TestCaseSource(typeof(TestCaseSources), nameof(TestCaseSources.SimpleTestCases))]
    public void SimpleMemoryUsage(string input)
    {
        long memoryBefore = GC.GetTotalMemory(forceFullCollection: true);

        string cipherText = _mathCrpyt.Encrypt(input);

        long memoryBetween = GC.GetTotalMemory(forceFullCollection: true);

        string plainText = _mathCrpyt.Decrypt(cipherText);

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
