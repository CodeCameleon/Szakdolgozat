using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Session;
using Microsoft.Extensions.DependencyInjection;
using TestResults.Entities;
using TestResults.Services.Interfaces;
using Shared.Algorithms.Interfaces;

namespace Tests.Algorithm.MemoryUsageTests;

/// <summary>
/// A memória használat mérését végző absztrakt osztály.
/// </summary>
[TestFixture]
[NonParallelizable]
internal abstract class BaseMemoryUsage<Algorithm>
    where Algorithm : IEncryptionAlgorithm, new()
{
    /// <summary>
    /// A titkosító algoritmust tároló adattag.
    /// </summary>
    private IEncryptionAlgorithm _algorithm;

    /// <summary>
    /// A memóriahasználat eredményeket kezelő szolgáltatást tároló adattag.
    /// </summary>
    private IMemoryUsageResultService _memoryUsageResultService;

    /// <summary>
    /// A teszteket előkészítő függvény.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _algorithm = new Algorithm();

        _memoryUsageResultService = DatabaseSetUp.ServiceProvider.GetRequiredService<IMemoryUsageResultService>();
    }

    /// <summary>
    /// A teszteket lezáró függvény.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _algorithm.Dispose();
    }

    /// <summary>
    /// A memória használat mérését végző függvény.
    /// </summary>
    /// <param name="input">A titkosítandó szöveg.</param>
    protected void MemoryUsage(string input)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        long memoryBefore = GC.GetTotalMemory(true);

        string cipherText = _algorithm.Encrypt(input);

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        long memoryBetween = GC.GetTotalMemory(true);

        string plainText = _algorithm.Decrypt(cipherText);

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        long memoryAfter = GC.GetTotalMemory(true);

        long encryptionMemoryUsage = Math.Max(0, memoryBetween - memoryBefore);
        long decryptionMemoryUsage = Math.Max(0, memoryAfter - memoryBetween);

        _memoryUsageResultService.CreateAsync(new MemoryUsageResult
        {
            AlgorithmName = _algorithm.GetType().Name,
            Input = input,
            IsSuccessful = plainText.Equals(input),
            EncryptionMemoryUsage = encryptionMemoryUsage,
            DecryptionMemoryUsage = decryptionMemoryUsage
        });

        Assert.That(plainText, Is.EqualTo(input));
    }

    /// <summary>
    /// A titkosítás memória használatának mérését végző függvény.
    /// Ez a függvény a Microsoft.Diagnostics.Tracing könyvtár segítségével méri a memória használatot.
    /// </summary>
    /// <param name="input">A titkosítandó szöveg.</param>
    protected async Task TraceEventEncryptionMemoryUsage(string input)
    {
        long memoryUsage = 0;

        using TraceEventSession session = new($"MemorySession-{Guid.NewGuid()}");

        session.EnableProvider(
            ClrTraceEventParser.ProviderGuid,
            TraceEventLevel.Verbose,
            (ulong)ClrTraceEventParser.Keywords.GC
        );

        Task processingTask = Task.Run(session.Source.Process);

        session.Source.Clr.All += data =>
        {
            if (data.EventName.Equals("GC/AllocationTick"))
            {
                memoryUsage += (long)data.PayloadByName("AllocationAmount64");
            }
        };

        string cipherText = _algorithm.Encrypt(input);

        await Task.Delay(100);

        session.Stop();

        TestContext.Out.WriteLine($"A titkosítás {memoryUsage} bájt memóriát használt.");

        Assert.Multiple(() =>
        {
            Assert.That(memoryUsage, Is.GreaterThan(0));
            Assert.That(cipherText, Is.Not.Null);
        });

        await processingTask;
    }
}
