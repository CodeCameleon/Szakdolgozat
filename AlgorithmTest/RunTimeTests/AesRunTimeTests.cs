using AlgorithmTest.Models;

namespace AlgorithmTest.RunTimeTests;

/// <summary>
/// Az AES titkosító algoritmus futási idejét vizsgáló tesztesetek.
/// </summary>
internal class AesRunTimeTests
    : BaseRunTime<AesAlgorithm>
{
    /// <summary>
    /// Az egyszerű tesztesetek futási idejét vizsgáló teszt.
    /// </summary>
    /// <param name="input">A titkosítandó szöveg.</param>
    [Test, TestCaseSource(typeof(TestCaseSources), nameof(TestCaseSources.SimpleTestCases))]
    public void SimpleRunTime(string input)
    {
        RunTime(input);
    }
}
