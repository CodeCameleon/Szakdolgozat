using AlgorithmTest.Models;

namespace AlgorithmTest.MemoryUsageTests;

/// <summary>
/// A DES titkosító algoritmus memória használatát vizsgáló tesztesetek.
/// </summary>
internal class DesMemoryUsageTests
    : BaseMemoryUsage<DesAlgorithm>
{
    /// <summary>
    /// Az egyszerű tesztesetek memória használatát vizsgáló teszt.
    /// </summary>
    /// <param name="input">A titkosítandó szöveg.</param>
    [Test, TestCaseSource(typeof(TestCaseSources), nameof(TestCaseSources.SimpleTestCases))]
    public void Simple(string input)
    {
        MemoryUsage(input);
    }

    /// <summary>
    /// Az egyszerű tesztesetek titkosításának memória használatát vizsgáló teszt.
    /// </summary>
    /// <param name="input">A titkosítandó szöveg.</param>
    [Ignore("Nem megfelelő.")]
    [Test, TestCaseSource(typeof(TestCaseSources), nameof(TestCaseSources.SimpleTestCases))]
    public async Task SimpleTraceEventEncryption(string input)
    {
        await TraceEventEncryptionMemoryUsage(input);
    }
}
