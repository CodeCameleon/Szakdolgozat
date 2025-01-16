using Base.Test.Implementations;

namespace AlgorithmTest.RunTimeTests;

/// <summary>
/// Az AES titkosító algoritmus futási idejét vizsgáló tesztesetek.
/// </summary>
internal class AesRunTimeTests
    : BaseRunTime<AesAlgorithm>
{
    /// <summary>
    /// Az összes teszteset futási idejét vizsgáló teszt.
    /// </summary>
    /// <param name="input">A titkosítandó szöveg.</param>
    [Test, TestCaseSource(typeof(TestCaseSources), nameof(TestCaseSources.AllTestCases))]
    public void All(string input)
    {
        RunTime(input);
    }
}
