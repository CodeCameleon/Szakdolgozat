using Base.Test.Implementations;

namespace AlgorithmTest.MemoryUsageTests;

/// <summary>
/// Az AES titkosító algoritmus memória használatát vizsgáló tesztesetek.
/// </summary>
internal class AesMemoryUsageTests
    : BaseMemoryUsage<AesAlgorithm>
{
    /// <summary>
    /// Az összes teszteset memória használatát vizsgáló teszt.
    /// </summary>
    /// <param name="input">A titkosítandó szöveg.</param>
    [Test, TestCaseSource(typeof(TestCaseSources), nameof(TestCaseSources.AllTestCases))]
    public void All(string input)
    {
        MemoryUsage(input);
    }

    /// <summary>
    /// Az összes teszteset titkosításának memória használatát vizsgáló teszt.
    /// </summary>
    /// <param name="input">A titkosítandó szöveg.</param>
    [Ignore("Nem megfelelő.")]
    [Test, TestCaseSource(typeof(TestCaseSources), nameof(TestCaseSources.AllTestCases))]
    public async Task AllTraceEventEncryption(string input)
    {
        await TraceEventEncryptionMemoryUsage(input);
    }
}
